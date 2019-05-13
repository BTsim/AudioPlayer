using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    static class Shuffle
    {
        public static List<Song> Shuffling(this List<Song> songs)
        {
            Random random = new Random();
            List<Song> newSongs = new List<Song>();
            List<int> nums = new List<int>();
            int i = 0;
            int j = 0;
            int k = 0;
            foreach (Song song in songs)
            {
                while (k!=1)
                {
                    i = random.Next(0, songs.Count);
                    if (nums.Contains(i))
                    {
                        k = 0;
                    }
                    else
                    {
                        nums.Add(i);
                        k = 1;
                    }
                }
                newSongs.Add(songs[i]);
                k = 0;
            }
            return newSongs;
        }
    }
}
