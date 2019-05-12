using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Player
    {
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
        
        private bool Locked;
        
        public Song[] Songs;

        public Skin skin;
        public Player(Skin skin)
        {
            this.skin = skin;
        }

        public void Play()
        {
            for (int i = 0; i < Songs.Length; i++)
            {
                Console.WriteLine(Songs[i].Title+" "+Songs[i].Artist.Name);
                System.Threading.Thread.Sleep(500);
            }
            
        }
        public void VolumeUp()
        {
            Volume = Volume + 1;
            Console.WriteLine("Volume icreased at 1. Current volume: "+Volume);
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
                Console.WriteLine("Volume icreased at "+volume_step+". Current volume: " + Volume);
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
            if (Locked==false)
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
        private List<Song> songs = new List<Song>();

        public void Play(bool loop = false)
        {
            int repeat;
            repeat = loop == false ? 1 : songs.Count;
            for (int i = 0; i < repeat; i++)
            {
                if (songs[i].like == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (songs[i].like==false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine(songs[i].Title+"Genre: "+songs[i].Genre);
                System.Threading.Thread.Sleep(2000);
            }
        }

        public void Add(List<Song> songs)
        {
            this.songs = songs;
        }

        public void Shuffle(List<Song> songs)
        {
            List<Song> newSongs = new List<Song>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = i; j < songs.Count; j += 3)
                {
                    newSongs.Add(songs[j]);
                }
            }
            this.songs = newSongs;
        }

        public void SortByTitle(List<Song> songs)
        {
            var songsTitle = new List<string>();
            foreach (Song song in songs)
            {
                string title = song.Title;
                songsTitle.Add(title);
            }
            songsTitle.Sort();
            var sortedSongs = new List<Song>();
            for (int i = 0; i < songsTitle.Count; i++)

            {
                foreach (Song song in songs)
                {
                    if (songsTitle[i] == song.Title)
                    {
                        sortedSongs.Add(song);
                    }
                }
            }
            this.songs = sortedSongs;
        }

        public void WriteLyrics(Song song)
        {
            song.Title = song.Title.Length > 13 ? song.Title.Remove(13, (song.Title.Length-13)) + " ..." : song.Title;
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

        public void FilterByGenre(List<Song> songs, Song.Genres genreFilter)
        {
            List<Song> genreFilteredSongs=new List<Song>();
            for (int i = 0; i < songs.Count; i++)
            {
                Song.Genres genreOfSong = songs[i].Genre;
                if (genreFilter == genreOfSong)
                {
                    genreFilteredSongs.Add(songs[i]);
                }

            }

            this.songs = genreFilteredSongs;
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

    abstract class Skin

    {

        public abstract void Clear();

        public abstract void Render(string text);

    }

    class ClassicSkin : Skin

    {

        public override void Clear()

        {

            Console.Clear();

        }

        public override void Render(string text)

        {

            Console.WriteLine(text);

        }

    }

    class ColorSkin : Skin
    {
        public ConsoleColor colorText;
        public ColorSkin(ConsoleColor color)
        {
            this.colorText = color;
        }
        public override void Clear()
        {
            Console.Clear();
        }
        public override void Render(string text)
        {
            Console.ForegroundColor = colorText;
            Console.WriteLine(text);
        }
    }

    class RandomSkin : Skin
    {
        public override void Clear()
        {
            Console.Clear();
            for (int i = 0; i < 30; i++)
            {
                Console.Write((char)058D);
            }
            Console.WriteLine();
        }
        public override void Render(string text)
        {
            Random rand = new Random();
            Console.ForegroundColor = (ConsoleColor)rand.Next(0, 15);
            Console.WriteLine(text);
        }
    }
}
