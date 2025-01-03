﻿using System;

namespace PrepExam4
{
    public class Game
    {
        public string Platform { get; set; }
        public string Played { get; set; }
        public string Name { get; set; }
        public int OpenCritic { get; set; }
        public string Genre { get; set; }
        public string Multiplayer { get; set; }
        public string Source { get; set; }
        public Game(string line)
        {
            try
            {
                string[] parts = line.Split(';');
                Platform = parts[0];
                Played = parts[1];
                Name = parts[2];
                OpenCritic = int.Parse(parts[3]);
                Genre = parts[4];
                Multiplayer = parts[5];
                Source = parts[6];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Game(): {ex}");
            }
        }
        public override string ToString()
        {
            return string.Format($"{Platform,-15} {Played,-15} {OpenCritic,-15} {Multiplayer,-15} {Name}");
        }
    }

    public class GameStore
    {
        public Game[] games { get; private set; }
        public GameStore(string path)
        {
            Console.WriteLine("2nd task:\nReading file...");
            string[] lines = File.ReadAllLines(path);
            games = new Game[lines.Length - 1];
            int index = 0;
            foreach (string line in lines)
            {
                if (line != lines[0]) { games[index++] = new Game(line); }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string path = "ps_extra_games_input.csv";
            GameStore store = new GameStore(path);
            ShowData(store.games);
            PS45Games(store.games);
        }

        static void ShowData(Game[] games)
        {
            Console.WriteLine("3rd task:");
            Console.WriteLine($"{"Platform",-15}{"Played",-15}{"OpenCritic",-15}{"Multiplayer",-15}{"Name"}");
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(games[i]);
            }
        }
        static void PS45Games(Game[] games)
        {
            Console.WriteLine("\n4th task:");
            foreach (Game game in games)
            {
                if (game.Platform == "PS4/PS5" && game.OpenCritic >= 90)
                {
                    Console.WriteLine(game);
                }
            }
        }
    }
}
