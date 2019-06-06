using System;

namespace AudioPlayer
{
    [Serializable]
    public class Artist
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Country { get; set; } 
        public Artist(string name)
        {
            this.Name = name;
        }
        public Artist()
        {
            Name = "Unknown name";
        }
    }
}
