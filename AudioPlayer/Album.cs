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
        public string Name { get; set;}
        private string Path;
        public int Year { get; set; }
        public Album()
        {
            this.Name = "Unknown Album";
            this.Year = '-';
        }
        public Album(string Name, string Year)
        {
            Name = Name;
            Year = Year;
        }
    }
}
