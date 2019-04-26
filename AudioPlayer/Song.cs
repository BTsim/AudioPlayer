using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Song
    {
        public enum Genres
        {
            NoGenre=0,
            Pop=1,
            Rap=2,
            Rock=3,
            Electro=4,
            HipHop=5,
        }

        public int Duration;
        public string Title;
        public string Path;
        public string Lyrics;
        public Genres Genre;
        public Artist Artist;
        public bool? like=null;
        Album[] Album;
        Playlist[] Playlist;

        public void Like()
        {
            like=true;
        }

        public void Dislike()
        {
            like = false;
        }




    }
}
