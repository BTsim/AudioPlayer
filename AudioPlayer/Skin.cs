using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public abstract class Skin
    {
        public abstract void Clear();
        public abstract void Render(string text);
        public abstract void Render(string text, ConsoleColor color);
    }
}
