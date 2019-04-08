using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Player
    {
        private int _volume;
        private int _maxVolume = 100;
        private int _minVolume = 0;
        public int volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if(value>_maxVolume)//value - значение которые используется в данном гетере/сетере, в данном случае volume
                {
                    _volume = _maxVolume;
                }
                else if(value<_minVolume)
                {
                    _volume = _minVolume;
                }
                else
                {
                    _volume = value;
                }
            }

        }
        
        bool islock;
        public Song[] songs; //связь один ко многим

        public void Play()
        {
            for (int i = 0; i < songs.Length; i++)
            {
                Console.WriteLine(songs[i].title);
                System.Threading.Thread.Sleep(500);
            }
            
        }
        public void VolumeUp()
        {
            volume = volume + 1;
            Console.WriteLine(volume);
        }
        public void VolumeDown()
        {
            volume = volume - 1;
            Console.WriteLine(volume);
        }


    }
}
