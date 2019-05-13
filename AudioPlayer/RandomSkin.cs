using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public class RandomSkin : Skin
    {
        public override void Clear()
        {
            Console.Clear();
            for (int i = 0; i < 30; i++)
            {
                Console.Write((char)058D);
            }
            Console.WriteLine();
        }
       
        public override void Render(string text)
        {
            Random rand = new Random();
            Console.ForegroundColor = (ConsoleColor)rand.Next(0, 15);
            Console.WriteLine(text);
        }
        public override void Render(string text, ConsoleColor color)
        {
            Random rand = new Random();
            Console.ForegroundColor = (ConsoleColor)rand.Next(0, 15);
            Console.WriteLine(text);
        }
    }
}
