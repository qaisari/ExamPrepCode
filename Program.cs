using System;

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
                //Console.WriteLine(line);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Game(): {ex}");
            }
        }
        public override string ToString()
        {
            return $"{Platform} {Played} {Name} {OpenCritic} {Genre} {Multiplayer} {Source}";
        }
    }

    public class GameStore
    {
        public Game[] games { get; private set; }
        public GameStore(string path)
        {
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
            foreach (Game game in store.games)
            {
                Console.WriteLine(game.ToString());
            }
        }
    }
}
