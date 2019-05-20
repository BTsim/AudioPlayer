using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    [Serializable]
    public class Album
    {
        public string Name { get; set;}
        public string Path { get; set; }
        public int Year { get; set; }
        public Album(string name, int year)
        {
            Name = name;
            Year = year;
        }
        public Album()
        {

        }
    }
}
