using System;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Background Changer App!");

            Console.WriteLine("[R]ed | [G]reen | [B]lue | E[x]it");

            var running = true;

            do
            {
                var input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.R:
                        break;
                    case ConsoleKey.G:
                        break;
                    case ConsoleKey.B:
                        break;
                    case ConsoleKey.X:
                        running = false;
                        break;
                    default: 
                        Console.WriteLine("No match input...");
                        break;
                }
            } while (running);
        }
    }
}