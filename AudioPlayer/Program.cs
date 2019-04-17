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
            var player = new Player();
            CreateSong();
            CreateSong("name of song3");

            CreateSong(1500, "name of song4", "title of song4", "path of song4", "lyrics of song4", "genre of song 4");

            player.Add(CreateSong("name of song"));

            player.Add(CreateSong("name of song1"), CreateSong("name of song2"));

            Song[] songs = new Song[10];
            for (int j= 0; j < songs.Length; j++)
            {
                var song =CreateSong("name of song" + (j + 1));
                songs[j]=song;
            }
            player.Add(songs);
            //var songs = CreateSongs(out min, out max, ref total);
            //player.songs = songs;
            //int MinDuration;
            //int MaxDuration;
            //int TotalDuration;
            //Console.WriteLine($"{min},{max},{total}");

            int i = 0;
            while (i < 2)
            {
                switch (ReadLine())
                {
                    case "up":
                        player.VolumeUp();
                        break;
                    case "down":
                        player.VolumeDown();
                        break;
                    case "play":
                        player.Play();
                        break;
                }
                i++;
            }
            //player.VolumeChange(Convert.ToInt32(ReadLine()));
            //player.Start();
            //player.Stop();
            //player.Lock();
            //player.Unlock();

            Console.ReadLine();

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
            song2.Genre = "Genre of song 2";
            return song2;
        }
        public static Song CreateSong(string nameOfSong3)
        {
            var song3 = new Song();
            song3.Duration = 1000;
            song3.Title = nameOfSong3;
            song3.Path = "Path of song3";
            song3.Lyrics = "Lyrics of song3";
            song3.Genre = "Genre of song 3";
            return song3;
        }
        public static Song CreateSong(int durationOfSong4, string nameOfSong4, string titleOfSong4, string pathOfSong4, string lyricsOfSong4, string genreOfSong4)
        {
            var song4 = new Song();
            song4.Duration = durationOfSong4;
            song4.Title = nameOfSong4;
            song4.Path = pathOfSong4;
            song4.Lyrics = lyricsOfSong4;
            song4.Genre = genreOfSong4;
            return song4;
        }
        //refactoring 
        public static Song CreateSong()
        {
            var song2 = new Song();
            CreateSong();
            return song2;
        }

    }
}
