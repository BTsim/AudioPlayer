using System;
using System.Collections.Generic;

namespace AudioPlayer
{
    [Serializable]
    public class Playlist
    {
        [NonSerialized]
        private string _path;
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
        public string Title { get; set; }
        public List<Song> Songs { get; set; }

        public Playlist()
        {

        }

        public Playlist(string title)
        {
            Title = title;
        }

        public Playlist(string title, List<Song> songs): this(title)
        {
            Songs = songs;
        }
    }
}
