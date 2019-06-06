using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using File = TagLib.File;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace AudioPlayer
{
    public class Player: IDisposable
    {
        public List<Playlist> Playlists { get; private set; } = new List<Playlist>();
        public List<Song> Songs { get; set; } = new List<Song>();
        private static Skin Skin { get; set; }
        public static bool Loop { get; set; }
        private static string _path;
        private bool _locked=false;
        public bool Locked {
            get
            {
                return _locked;
            }
            
            set
            {
                _locked = value;
            }
        }

        public event PlayerMessageHandler PlayerStarted;
        public event PlayerMessageHandler PlayerStopped;
        public event PlayerMessageHandler SongStarted;
        public event PlayerMessageHandler SongsListChanged;
        public event PlayerMessageHandler VolumeChanged;
        public event PlayerMessageHandler PlayerLocked;
        public event PlayerMessageHandler PlayerUnLocked;
        public event PlayerMessageHandler PlaylistLoad;
        public event PlayerMessageHandler PlaylistSave;

        private static SoundPlayer _soundPlayer=new SoundPlayer();
        private int _volume=50;
        private readonly int _maxVolume = 100;
        private readonly int _minVolume = 0;
        public int Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if (value > _maxVolume)
                {
                    _volume = _maxVolume;
                }
                else if (value < _minVolume)
                {
                    _volume = _minVolume;
                }
                else
                {
                    _volume = value;
                }
            }

        }
        private bool _disposed = false;
        public static Skin skin { get; set; }
        public Player(Skin skin)
        {
            Skin = skin;
        }

        public void Play(Song song)
        {
            _soundPlayer.SoundLocation = song.Path;
            _soundPlayer.Play();
        }
        public void VolumeUp()
        {
            Volume += 1;
            VolumeChanged(this, new PlayerEventArgs() { Message = "Volume turned up. New volume: " + Volume });
        }
        public void VolumeDown()
        {
            Volume -= 1;
            VolumeChanged(this, new PlayerEventArgs() { Message = "Volume turned down. New volume: " + Volume });
        }
        public void VolumeChange(int volume_step)
        {
            Volume += volume_step;
            if (volume_step > 0)
            {
                VolumeChanged(this, new PlayerEventArgs() { Message = "Volume turned up. New volume: " + Volume });
            }
            if (volume_step < 0)
            {
                VolumeChanged(this, new PlayerEventArgs() { Message = "Volume turned down. New volume: " + Volume });
            }
        }
        public void Lock()
        {
            Locked = true;
            PlayerLocked?.Invoke(this, new PlayerEventArgs() { Message = "Player locked" });
        }
        public void Unlock()
        {
            Locked = false;
            PlayerUnLocked?.Invoke(this, new PlayerEventArgs() { Message = "Player unlocked" });


        }
        public void Stop()
        {

            _soundPlayer.Stop();
            PlayerStopped(this, new PlayerEventArgs() { Message = "Player stopped" });

        }
        
        public static void WriteSongsList(List<Song> songs)
        {
            foreach (Song song in songs)
            {
                if (song.Play == true)
                {
                    WriteSongData(song, ConsoleColor.DarkRed);
                }
                else
                {
                    if (song.Liked.HasValue == true)
                    {
                        if (song.Liked == true)
                        {
                            WriteSongData(song, ConsoleColor.Red);
                        }
                        else
                        {
                            WriteSongData(song, ConsoleColor.Green);
                        }
                    }
                    else WriteSongData(song, ConsoleColor.White);
                }
            }
        }
        public static void WriteSongData(Song song, ConsoleColor color)
        {
            var (title, minutes, seconds, artistName, album, year) = song;
            Skin.Render($"Title - {title.TrimSongTitle()}", color);
            Skin.Render($"Duration - {minutes}.{seconds}");
            Skin.Render($"Artist - {artistName}");
            Skin.Render($"Album - {album}");
            Skin.Render($"Year - {year}");
            Console.WriteLine();
        }

        private static void GetPath()
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    _path = folder.SelectedPath;
                }
            }
        }
        public void Load()
        {
            PlayerStarted?.Invoke(this, new PlayerEventArgs { Message = "Enter the way to folder with music" });
            List<FileInfo> fileInfos = new List<FileInfo>();
            Thread thread = new Thread(GetPath);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            DirectoryInfo directoryInfo = new DirectoryInfo(_path);
            foreach (var file in directoryInfo.GetFiles("*.wav"))
            {
                fileInfos.Add(file);
            }
            foreach (var file in fileInfos)
            {
                var audio = File.Create(file.FullName);
                Songs.Add(new Song() { Album = new Album(audio?.Tag.Album, (int)audio.Tag?.Year), Artist = new Artist(audio.Tag?.FirstPerformer), Duration = (int)audio.Properties.Duration.TotalSeconds, Genre = audio.Tag?.FirstGenre, Lyrics = audio.Tag?.Lyrics, Title = audio.Tag?.Title, Path = audio.Name });
            }
            SongsListChanged?.Invoke(this, new PlayerEventArgs() { Message = "New playlist loaded" });

        }

        public void LoadPlayList()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(Playlists.GetType());
            using (FileStream fs = new FileStream("Playlists.xml", FileMode.Open))
            {
                Playlists = (List<Playlist>)xmlSerializer.Deserialize(fs);
            }
            PlaylistLoad?.Invoke(this, new PlayerEventArgs() { Message = "Enter the number of playlist for playing" });
            int i = 1;
            foreach (var playlist in Playlists)
            {
                Skin.Render($"{i}. {playlist.Title}");
            }
            int number = int.Parse(Console.ReadLine());
            Songs = Playlists[number - 1].Songs;
            PlaylistLoad?.Invoke(this, new PlayerEventArgs() { Message = "Playlis loaded" });
        }

        public void SaveAsPlaylist()
        {
            PlaylistSave(this, new PlayerEventArgs() { Message = "Enter the name of playlist" });
            Playlist playlist = new Playlist(Console.ReadLine(), Songs);
            Playlists.Add(playlist);
            XmlSerializer xmlSerializer = new XmlSerializer(Playlists.GetType());
            using (FileStream fs = new FileStream("Playlists.xml", FileMode.OpenOrCreate))
            {
                playlist.Path = fs.Name;
                xmlSerializer.Serialize(fs, Playlists);
            }
            PlaylistSave(this, new PlayerEventArgs() { Message = "Playlist saved" });
        }

        public void Clear()
        {
            Songs.Clear();
            skin.Render("List of songs is cleared");
        }

        public void WriteLyrics(Song song)
        {
            song.Title = song.Title.Length > 13 ? song.Title.Remove(13, (song.Title.Length - 13)) + " ..." : song.Title;
            skin.Render(song.Title);
            if (song.Lyrics != null)
            {
                string[] massStringLyrics = song.Lyrics.Split(';');
                for (int i = 0; i < massStringLyrics.Length; i++)
                {
                    skin.Render(massStringLyrics[i]);
                }
            }
        }

        public static List<Song> FilterByGenre(List<Song> songs, string genre)
        {
            List<Song> filteredSongs = new List<Song>();
            foreach (Song song in songs)
            {
                if (song.Genre == genre)
                {
                    filteredSongs.Add(song);
                }
            }
            return filteredSongs;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool isDispose)
        {
            if (!_disposed)
            {
                if (isDispose)
                {
                    _soundPlayer = null;
                    Playlists = null;
                    Songs = null;
                    Skin = null;
                }
                _soundPlayer.Dispose();
                _soundPlayer = null;
                _disposed = true;
            }
        }
        ~Player()
        {
            Dispose(false);
        }

        public delegate void PlayerMessageHandler(Player player, PlayerEventArgs arg);

    }
    static class ExtMethods
    {
        public static List<Song> ShuffleExtension(this List<Song> songs)
        {
            List<Song> songsNew = new List<Song>();
            for (int i = 0; i < songs.Count; i++)
            {
                for (int j = 0; j < songs.Count; j++)
                {
                    songsNew.Add(songs[j]);
                }
            }

            return songs = songsNew;
        }

        public static List<Song> SortByTitleExtension(this List<Song> songs)
        {
            var titleOfSongs = new List<string>();
            foreach (Song song in songs)
            {
                string title = song.Title;
                titleOfSongs.Add(title);
            }
            titleOfSongs.Sort();
            var songsSortedTitle = new List<Song>();
            for (int i = 0; i < titleOfSongs.Count; i++)
            {
                foreach (Song song in songs)
                {
                    if (titleOfSongs[i] == song.Title);
                }
            }

            return songs = songsSortedTitle;
        }

        public static string TrimSongTitle(this string title)
        {
            title = title?.Length > 13 ? title.Remove(13, (title.Length - 13)) : title;
            return title;
        }
    }

}
