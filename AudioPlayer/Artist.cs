using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Artist
    {
        public string Name;
        public string Nickname;
        public string Country;
        public Artist()
        {
            this.Name = "Unknown artist";
        }
        public Artist(string name)
        {
            this.Name = name;
        }
    }
}
