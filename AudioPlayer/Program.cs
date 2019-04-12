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
            var song1 = new Song
            {
                title = "Дым сигарет с ментолом",
                duration = 300,
                artist = new Artist

                {
                    name = "Нэнси"
                }
            };
            var song2 = new Song
            {
                title = "Anaconda",
                duration = 270,
                artist = new Artist
                {
                    name = "Nicki Minaj"
                }
            };
            var player = new Player
            {
                songs = new[] { song1, song2 }
            };

            int i = 0;
            while (i<2)
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
    }
}
