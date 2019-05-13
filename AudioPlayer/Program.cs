using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AudioPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            int min, max, total = 0;
            Skin skin = new RandomSkin();
            var player = new Player(skin);
            List<Song> songs = new List<Song>();

            int i;
            for (i = 0; i < 8; i++)
            {
                var song = CreateSong("song " + (i + 1));
                songs.Add(song);
            }

            player.Add(songs);
            Sort.SortByTitle(songs);
            Shuffle.Shuffling(songs);
        }

        private static Song[] CreateSongs( out int min, out int max, ref int total)
        {
            Random rand = new Random();
            Song[] songs = new Song[5];
            int MinDuration=0, MaxDuration=0, TotalDuration=0;
            for (int i = 0; i < songs.Length; i++)
            {
                var song1 = new Song();
                song1.Title = "Song" + i;
                song1.Duration = rand.Next(3000);
                song1.Artist = new Artist("Nensi");
                songs[i] = song1;
                TotalDuration = TotalDuration + song1.Duration;
                if (MaxDuration < song1.Duration)
                {
                    MaxDuration = song1.Duration;
                }
                if (MinDuration>song1.Duration)
                {
                    MinDuration = song1.Duration;
                }
            }
            min = MinDuration;
            max = MaxDuration;
            total = TotalDuration;
            return songs;
          
        }
        public static Song CreateSong()
        {
            var song2 = new Song();
            song2.Duration = 500;
            song2.Title = "Title of song2";
            song2.Path = "Path of song2";
            song2.Lyrics = "Lyrics of song2";
            song2.Genre = (Song.Genres) 1;
            return song2;
        }
        public static Song CreateSong(string nameOfSong3)
        {
            var song3 = new Song();
            song3.Duration = 1000;
            song3.Title = nameOfSong3;
            song3.Path = "Path of song3";
            song3.Lyrics = "Lyrics of song3";
            song3.Genre = (Song.Genres)2;
            return song3;
        }
        public static Song CreateSong(int durationOfSong4, string nameOfSong4, string titleOfSong4, string pathOfSong4, string lyricsOfSong4, string genreOfSong4)
        {
            var song4 = new Song();
            song4.Duration = durationOfSong4;
            song4.Title = nameOfSong4;
            song4.Path = pathOfSong4;
            song4.Lyrics = lyricsOfSong4;
            song4.Genre = (Song.Genres)3;
            return song4;
        }
        //refactoring 
        // public static Song CreateSong()
        //{
        //var song2 = new Song();
        //CreateSong();
        //return song2;
        //}
        public static Artist AddArtist(string name = "Unknown Artist")
        {
            var artist = new Artist("Unknown Artist");
            artist.Name = name;
            WriteLine(artist.Name);
            return artist;
        }
        public static Album AddAlbum(int year = 0, string name = "Unknown Album")
        {
            var album = new Album("Unknown Album", "Unknown year");
            album.Name = name;
            album.Year = year;
            WriteLine($"Name of album:{album.Name}, Year of album: {album.Year}");
            return album;
        }
    }
}
