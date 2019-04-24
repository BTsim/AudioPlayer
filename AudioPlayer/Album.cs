using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Album
    {
        public Song Song;
        public string Name;
        string Path;
        public int Year;
        public Album()
        {
            this.Name = "Unknown Album";
            this.Year = '-';
        }
        public Album(string Name, string Year)
        {
        }
    }
}
