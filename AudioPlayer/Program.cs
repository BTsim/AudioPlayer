using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var song1 = new Song();
            song1.title = "Дым сигарет с ментолом";
            song1.duration = 300;
            song1.artist = new Artist
            {
                name = "Нэнси"
            };
            var song2 = new Song();
            song2.title = "Anaconda";
            song2.duration = 270;
            song2.artist = new Artist
            {
                name = "Nicki Minaj"
            };
            var player = new Player();
            player.songs = new[] { song1, song2 };
        }
    }
}
