using System;
using System.Reflection.Metadata.Ecma335;
//wa fiiiiin
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
        static void Main()
        {
            string path = "ps_extra_games_input.csv";
            GameStore store = new GameStore(path);
            //task 3
            ShowData(store.games);
            //task 4
            List<Game> ps45Games = PS45Games(store.games);
            foreach (Game game in ps45Games)
            {
                Console.WriteLine(game);
            }
            Console.WriteLine($"A total of {ps45Games.Count} games are available for PS4 and PS5, which received a 90% rating:");
            foreach (Game game in ps45Games)
            {
                Console.WriteLine(game.Name);
            }
            //task 5
            var groupRating = GroupRatings(store.games);
            Console.WriteLine($"Excellent (91-100): {groupRating.Excellent}\nGood (81-90): {groupRating.Good}\nPlayable (71-80): {groupRating.Playable}\nBad (0-70): {groupRating.Bad}");
            //task 6
            var search = Search(store.games);
            if (search) { Console.WriteLine("The game you are looking for is available."); }
            else { Console.WriteLine("The game you're looking for doesn't exist"); }
            //task 7
            Game bestgame = BestGame(store.games);
            if (bestgame != null)
            {
                Console.WriteLine($"Top rated game: {bestgame.Name} ({bestgame.OpenCritic})");
            }
            //task 8
            var multiplayer = Multiplayer(store.games);
            Console.WriteLine($"Online: {multiplayer.Online}\nLocal: {multiplayer.Local}\nBoth: {multiplayer.Both}");
        }
        //task 3
        static void ShowData(Game[] games)
        {
            Console.WriteLine("3rd task:");
            Console.WriteLine($"{"Platform",-15}{"Played",-15}{"OpenCritic",-15}{"Multiplayer",-15}{"Name"}");
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(games[i]);
            }
        }
        //task 4
        static List<Game> PS45Games(Game[] games)
        {
            Console.WriteLine("\n4th task:");
            List<Game> ps45Games = new List<Game>();
            foreach (Game game in games)
            {
                if (game.Platform == "PS4/PS5" && game.OpenCritic >= 90)
                {
                    ps45Games.Add(game);
                }
            }
            return ps45Games;
        }
        //task 5
        static (int Excellent, int Good, int Playable, int Bad) GroupRatings(Game[] games)
        {
            Console.WriteLine("\n5th task:");
            Console.WriteLine("Ratings:");
            int Excellent = 0, Good = 0, Playable = 0, Bad = 0;
            foreach (Game game in games)
            {
                if (game.OpenCritic > 90) { Excellent++; }
                else if (game.OpenCritic >= 81 && game.OpenCritic <= 90) { Good++; }
                else if (game.OpenCritic >= 71 && game.OpenCritic <= 80) { Playable++; }
                else { Bad++; }
            }
            return (Excellent, Good, Playable, Bad);
        }
        //task 6
        static bool Search(Game[] games)
        {
            Console.WriteLine("\n6th task:");
            Console.Write("Enter the name of the game you are looking for:");
            string name = Console.ReadLine();
            bool found = false;
            foreach (Game game in games)
            {
                if (game.Name == name) { found = true; }
            }
            return found;
        }
        //task 7
        static Game BestGame(Game[] games)
        {
            int bestGameRating = 0;
            Game bestGame = null;
            Console.WriteLine("\n7th task");
            foreach (Game game in games)
            {
                if (bestGameRating < game.OpenCritic) { bestGameRating = game.OpenCritic; bestGame = game; }
            }
            return bestGame;
        }
        //task 8
        static (int Online,int Local,int Both) Multiplayer(Game[] games)
        {
            Console.WriteLine("\n8th task:");
            Console.WriteLine("Multiplayer options:");
            int Online = 0;int Local = 0;int Both = 0;
            foreach (Game game in games)
            {
                if (game.Multiplayer == "Online") { Online++; }
                else if (game.Multiplayer == "Local") { Local++; }
                else { Both++; }
            }
            return (Online, Local, Both);
        }
        //it was nice to work on this
    }
}
