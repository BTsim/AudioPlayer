using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    [Serializable]
    public class Song
    {
        private Genres _genre;
        public enum Genres
        {
            NoGenre=0,
            Pop=1,
            Rap=2,
            Rock=3,
            Electro=4,
            HipHop=5,
        }

        public int Duration { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Lyrics { get; set; }
        public Artist Artist { get; set; }
        public Album Album { get; set; }
        public bool? Liked=null;
        private bool _play;
        public bool Play
        {
            get
            {
                return _play;
            }
            set
            {
                _play = value;
            }
        }
        public string Genre
        {
            get { return _genre.ToString(); }
            set
            {
                if (value != null)
                {
                    _genre = (Genres)Enum.Parse(typeof(Genres), value);
                }
                else
                {
                    value = "NotSpecified";
                    _genre = (Genres)Enum.Parse(typeof(Genres), value);
                }
            }
        }

        private Playlist[] Playlist;

        public void Like(Song song)
        {
            song.Liked=true;
        }

        public static void Dislike(Song song)
        {
            song.Liked = false;
        }

        public void Deconstruct(out string title, out int min, out int sec, out string nameOfArtist,
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
