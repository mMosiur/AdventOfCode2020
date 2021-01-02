using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{

    class Program
    {
        static int GetSumOfAny()
        {
            FileStream fs = new("input.txt", FileMode.Open);
            int sum = 0;
            using (StreamReader reader = new(fs))
            {
                var groups = reader.ReadToEnd().Split("\n\n").Select(str => str.Split('\n')).ToArray();
                foreach (var group in groups)
                {
                    HashSet<char> chars = new();
                    foreach (string answers in group)
                        foreach(char c in answers)
                            chars.Add(c);
                    sum += chars.Count;
                }
            }
            return sum;
        }

        static int GetSumOfAll()
        {
            FileStream fs = new("input.txt", FileMode.Open);
            int sum = 0;
            using (StreamReader reader = new(fs))
            {
                var groups = reader.ReadToEnd().Split("\n\n").Select(str => str.Split('\n')).ToArray();
                foreach (var group in groups)
                {
                    HashSet<char> chars = new("abcdefghijklmnopqrstuvwxyz");
                    foreach (string answers in group)
                        chars.IntersectWith(answers);
                    sum += chars.Count;
                }
            }
            return sum;
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"Any: {GetSumOfAny()}");
            Console.WriteLine($"Any: {GetSumOfAll()}");
        }
    }
}
