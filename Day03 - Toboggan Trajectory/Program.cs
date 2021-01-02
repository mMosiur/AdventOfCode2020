using System;
using System.IO;

namespace Day03
{
    class Program
    {
        static char[,] GetData()
        {
            char[,] map;
            using (StreamReader stream = new("input.txt"))
            {
                string[] lines = stream.ReadToEnd().Split('\n');
                map = new char[lines.Length - 1, lines[0].Length];
                for (int i = 0; i < lines.Length - 1; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        map[i, j] = lines[i][j];
                    }
                }
                return map;
            }
        }

        static int TreeEncounterCount(char[,] map, int downSlope, int sideSlope)
        {
            int count = 0;
            int i = downSlope;
            int j = sideSlope;
            while (i < map.GetLength(0))
            {
                char c = map[i, j];
                if (map[i, j] == '#') count++;
                i += downSlope;
                j += sideSlope;
                j %= map.GetLength(1);
            }
            return count;
        }

        static void Main(string[] args)
        {
            char[,] map = GetData();
            long result = 1;
            result *= TreeEncounterCount(map, 1, 1);
            result *= TreeEncounterCount(map, 1, 3);
            result *= TreeEncounterCount(map, 1, 5);
            result *= TreeEncounterCount(map, 1, 7);
            result *= TreeEncounterCount(map, 2, 1);
            Console.WriteLine(result);
        }
    }
}
