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
            var songs = CreateSongs(out min, out max, ref total);
            player.songs = songs;
            int MinDuration;
            int MaxDuration;
            int TotalDuration;
            Console.WriteLine($"{min},{max},{total}");

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
            player.VolumeChange(Convert.ToInt32(ReadLine()));
            player.Start();
            player.Stop();
            player.Lock();
            player.Unlock();

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
    }
}
