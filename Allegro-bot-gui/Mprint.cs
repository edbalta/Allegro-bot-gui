using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_bot_gui
{
    public class Mprint
    {
        public void SPrint(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[+] ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{text}");
            Console.ResetColor();
        }

        public void FPrint(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[!] ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {text}");
            Console.ResetColor();
        }

    }
}
