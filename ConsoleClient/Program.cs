using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Background Changer App!");

            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/hubs/background")
                .Build();

            connection.On<string>("changeBackground", (color) =>
            {
                switch (color.ToUpper())
                {
                    case "RED": Console.BackgroundColor = ConsoleColor.Red;
                        break;
                    case "GREEN": Console.BackgroundColor = ConsoleColor.Green;
                        break;
                    case "BLUE": Console.BackgroundColor = ConsoleColor.Blue;
                        break;
                    default: Console.BackgroundColor = ConsoleColor.Black;
                        break;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Changed color to {color}");

            });

            connection.Closed += exception =>
            {
                Console.WriteLine(exception);
                return Task.CompletedTask;
            };
            
            await connection.StartAsync();

            Console.WriteLine("[R]ed | [G]reen | [B]lue | E[x]it");

            var running = true;

            do
            {
                var input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.R:
                        await connection.SendAsync("ChangeBackground", "red");
                        break;
                    case ConsoleKey.G:
                        await connection.SendAsync("ChangeBackground", "green");
                        break;
                    case ConsoleKey.B:
                        await connection.SendAsync("ChangeBackground", "blue");
                        break;
                    case ConsoleKey.X:
                        running = false;
                        break;
                    default: 
                        Console.WriteLine("No match input...");
                        break;
                }
            } while (running);

            await connection.StopAsync();
        }
    }
}