using System;
using System.Collections.Generic;

namespace AudioPlayer
{
    public class Sort
    {
        public static List<Song> SortByTitle(List<Song> songs)
        {
            var songsTitle = new List<string>();
            foreach (Song song in songs)
            {
                string title = song.Title;
                songsTitle.Add(title);
            }
            songsTitle.Sort();
            var sortedSongs = new List<Song>();
            for (int i = 0; i < songsTitle.Count; i++)

            {
                foreach (Song song in songs)
                {
                    if (songsTitle[i] == song.Title)
                    {
                        sortedSongs.Add(song);
                    }
                }
            }
           return sortedSongs;
        }
    }
}
