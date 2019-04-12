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
        public int Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if (value > _maxVolume)//value - значение которые используется в данном гетере/сетере, в данном случае volume
                {
                    _volume = _maxVolume;
                }
                else if (value < _minVolume)
                {
                    _volume = _minVolume;
                }
                else
                {
                    _volume = value;
                }
            }

        }
        private bool _playing;
        public bool Playing
        {
            get
            {
                return _playing;
            }
            private set
            {
                _playing = value;
            } 
        }
        
        bool Locked;
        
        public Song[] songs; //связь один ко многим

        public void Play()
        {
            for (int i = 0; i < songs.Length; i++)
            {
                Console.WriteLine(songs[i].Title+" "+songs[i].Artist.Name);
                System.Threading.Thread.Sleep(500);
            }
            
        }
        public void VolumeUp()
        {
            Volume = Volume + 1;
            Console.WriteLine("Volume icreased at 1. Current volume: "+Volume);
        }
        public void VolumeDown()
        {
            Volume = Volume - 1;
            Console.WriteLine("Volume decreased at 1. Current volume: " + Volume);
        }
        public void VolumeChange(int volume_step)
        {
            Volume = Volume + volume_step;
            if (volume_step > 0)
            {
                Console.WriteLine("Volume icreased at "+volume_step+". Current volume: " + Volume);
            }
            if (volume_step < 0)
            {
                Console.WriteLine("Volume decreased at " + volume_step + ". Current volume: " + Volume);
            }
        }
        public void Lock()
        {
            Locked = true;
            Console.WriteLine("Player locked");
        }
        public void Unlock()
        {
            Locked = false;
            Console.WriteLine("Player unlocked");

        }
        public void Stop()
        {
            if (Locked==false)
            {
                Playing = false;
                Console.WriteLine("Stop");

            }
        }
        public void Start()
        {
            if (Locked == false)
            {
                Playing = true;
                Console.WriteLine("Playing");

            }

        }

    }
}
