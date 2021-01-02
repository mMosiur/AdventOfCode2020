using System;
using System.IO;
using System.Linq;
using StringExtensionMethods;

namespace Day22
{
    class Program
    {

        static (Deck, Deck) GetInput(string path)
        {
            FileStream filestream = null;
            try
            {
                filestream = new FileStream(path, FileMode.Open);
                using (StreamReader reader = new StreamReader(filestream))
                {
                    var (p1, p2) = reader.ReadToEnd().SplitIntoTwo("\n\n", StringSplitOptions.TrimEntries);
                    Deck deck1 = new Deck(p1.Split('\n').Skip(1).Select(str => (Card)int.Parse(str)));
                    Deck deck2 = new Deck(p2.Split('\n').Skip(1).Select(str => (Card)int.Parse(str)));
                    return (deck1, deck2);
                }
            }
            finally
            {
                if (filestream != null)
                {
                    filestream.Dispose();
                }
            }


        }

        static Deck Winner(Deck deck1, Deck deck2)
        {
            if (deck1.Count > 0 && deck2.Count == 0) return deck1;
            if (deck1.Count == 0 && deck2.Count > 0) return deck2;
            return null;
        }

        static void Main(string[] args)
        {
            var (deck1, deck2) = GetInput("input.txt");
            IGame game = new Game1(deck1.GetCopy(), deck2.GetCopy());
            game.Play();
            int score1 = game.Winner.Score;
            System.Console.WriteLine($"Part 1: {score1}");
            game = new Game2(deck1.GetCopy(), deck2.GetCopy());
            game.Play();
            int score2 = game.Winner.Score;
            System.Console.WriteLine($"Part 2: {score2}");
        }
    }
}
