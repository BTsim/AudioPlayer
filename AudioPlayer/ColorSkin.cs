using System;

namespace AudioPlayer
{
    public class ColorSkin : Skin
    {
        public ConsoleColor colorText;
        public ColorSkin(ConsoleColor color)
        {
            this.colorText = color;
        }
        public override void Clear()
        {
            Console.Clear();
        }
        public override void Render(string text)
        {
            Console.WriteLine(text);
        }
        public override void Render(string text, ConsoleColor color)
        {
            Console.ForegroundColor = colorText;
            Console.WriteLine(text);
        }
    }
}
