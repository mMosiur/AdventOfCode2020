using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
{
    struct Bag
    {
        public Bag(string color)
        {
            Color = color;
            Content = new();
        }
        public string Color { get; private set; }
        private Dictionary<Bag, int> Content { get; set; }

        public bool Contains(string color)
        {
            foreach (var bag in Content.Keys)
            {
                if (bag.Color == color) return true;
                if (bag.Contains(color)) return true;
            }
            return false;
        }

        public int Count
        {
            get
            {
                int count = 0;
                foreach (var pair in Content)
                {
                    count += (pair.Key.Count + 1) * pair.Value;
                }
                return count;
            }
        }

        public void Add(Bag bag, int count)
        {
            Content.Add(bag, count);
        }
    }

    static class Bags
    {
        static private Dictionary<string, Bag> bags = new();

        static public Bag GetBag(string color)
        {
            if (!bags.ContainsKey(color))
                bags[color] = new Bag(color);
            return bags[color];
        }

        public static int CountThatContain(string color)
        {
            return bags.Values.Count(bag => bag.Contains(color));
        }

        public static int CountBagsIn(string color)
        {
            Bag bag = GetBag(color);
            return bag.Count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new("input.txt", FileMode.Open);
            using (StreamReader reader = new(fs))
            {
                string[] lines = reader.ReadToEnd().Split('\n');
                foreach (string line in lines)
                {
                    string[] strPart = line.Split(' ', 5);
                    string color = strPart[0] + " " + strPart[1];
                    Bag bag = Bags.GetBag(color);
                    strPart = strPart[4].Split(' ', 5);
                    while (strPart.Length >= 4)
                    {
                        int count = int.Parse(strPart[0]);
                        color = strPart[1] + " " + strPart[2];
                        bag.Add(Bags.GetBag(color), count);
                        if(strPart.Length == 4) break;
                        strPart = strPart[4].Split(' ', 5);
                    }
                }
            }
            System.Console.WriteLine(Bags.CountThatContain("shiny gold"));
            System.Console.WriteLine(Bags.CountBagsIn("shiny gold"));
        }
    }
}
