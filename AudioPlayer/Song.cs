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
        public Album Album { get; set; }
        private Playlist[] Playlist;

        public void Like()
        {
            like=true;
        }

        public void Dislike()
        {
            like = false;
        }

        public void SongDeconstruct(out string title, out int min, out int sec, out string nameOfArtist,
            out string album, out int year)
        {
            title = Title;
            min = Duration / 60;
            sec = Duration % 60;
            nameOfArtist = Artist.Name;
            album = Album.Name;
            year = Album.Year;
        }
    }
}
