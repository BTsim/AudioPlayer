using System;

namespace AudioPlayer
{
    public abstract class Skin
    {
        public abstract void Clear();
        public abstract void Render(string text);
        public abstract void Render(string text, ConsoleColor color);
    }
}
