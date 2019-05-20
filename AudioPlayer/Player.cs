using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using File = TagLib.File;

namespace AudioPlayer
{
    class Player
    {
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();
        public static List<Song> Songs { get; set; } = new List<Song>();
        private static Skin Skin { get; set; }
        public static bool Loop { get; set; }
        public bool Locked { get; set; }
        private int _volume;
        private int _maxVolume = 100;
        private int _minVolume = 0;
        public int Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if (value > _maxVolume)//value - значение которые используется в данном гетере/сетере, в данном случае volume
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
        private bool _playing;
        public bool Playing
        {
            get
            {
                return _playing;
            }
            private set
            {
                _playing = value;
            }
        }



        public static Skin skin { get; set; }
        public Player(Skin skin)
        {
            Skin = skin;
        }

        public void Play(List<Song> songs, bool loop)
        {
            foreach (Song song in songs)
            {
                if (loop == true)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        song.Play = true;
                        Player.WriteSongsList(songs);
                    }
                }
                else
                {
                    song.Play = true;
                    Player.WriteSongsList(songs);
                    song.Play = false;
                    Console.ReadLine();
                }
            }
            
        }

        public void VolumeUp()
        {
            Volume = Volume + 1;
            Console.WriteLine("Volume icreased at 1. Current volume: " + Volume);
        }
        public void VolumeDown()
        {
            Volume = Volume - 1;
            Console.WriteLine("Volume decreased at 1. Current volume: " + Volume);
        }
        public void VolumeChange(int volume_step)
        {
            Volume = Volume + volume_step;
            if (volume_step > 0)
            {
                Console.WriteLine("Volume icreased at " + volume_step + ". Current volume: " + Volume);
            }
            if (volume_step < 0)
            {
                Console.WriteLine("Volume decreased at " + volume_step + ". Current volume: " + Volume);
            }
        }
        public void Lock()
        {
            Locked = true;
            Console.WriteLine("Player locked");
        }
        public void Unlock()
        {
            Locked = false;
            Console.WriteLine("Player unlocked");

        }
        public void Stop()
        {
            if (Locked == false)
            {
                Playing = false;
                Console.WriteLine("Stop");

            }
        }
        public void Start()
        {
            if (Locked == false)
            {
                Playing = true;
                Console.WriteLine("Playing");

            }

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

        public void Load()
        {
            List<FileInfo> fileInfos = new List<FileInfo>();
            Skin.Render("Enter the way to folder with music");
            string path = Console.ReadLine();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            foreach (var file in directoryInfo.GetFiles("*.wav"))
            {
                fileInfos.Add(file);
            }
            foreach (var file in fileInfos)
            {
                var audio = File.Create(file.FullName);
                Songs.Add(new Song() { Album = new Album(audio?.Tag.Album, (int)audio.Tag?.Year), Artist = new Artist(audio.Tag?.FirstPerformer), Duration = (int)audio.Properties.Duration.TotalSeconds, Genre = audio.Tag?.FirstGenre, Lyrics = audio.Tag?.Lyrics, Title = audio.Tag?.Title, Path = audio.Name });
            }

        }

        public void LoadPlayList()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(Playlists.GetType());
            using (FileStream fs = new FileStream("Playlists.xml", FileMode.Open))
            {
                Playlists = (List<Playlist>)xmlSerializer.Deserialize(fs);
            }
            Skin.Render("Enter the number of playlist for playing");
            int i = 1;
            foreach (var playlist in Playlists)
            {
                Skin.Render($"{i}. {playlist.Title}");
            }
            int number = int.Parse(Console.ReadLine());
            Songs = Playlists[number - 1].Songs;
        }

        public void SaveAsPlaylist()
        {
            Skin.Render("Enter the name of playlist");
            Playlist playlist = new Playlist(Console.ReadLine(), Songs);
            Playlists.Add(playlist);
            XmlSerializer xmlSerializer = new XmlSerializer(Playlists.GetType());
            using (FileStream fs = new FileStream("Playlists.xml", FileMode.OpenOrCreate))
            {
                playlist.Path = fs.Name;
                xmlSerializer.Serialize(fs, Playlists);
            }
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
