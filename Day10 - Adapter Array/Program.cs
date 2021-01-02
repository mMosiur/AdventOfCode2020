using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static int CountGapsOfWidth(int[] data, int width)
        {
            int count = 0;
            if (data[0] == width) count++;
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] - data[i - 1] == width)
                    count++;
            }
            return count;
        }

        static long CountDistinctRearrangements(int[] array, int source, int target)
        {
            SortedDictionary<int, long> counts = new();
            counts.Add(source, 1);
            foreach (int el in array)
                counts.Add(el, 0);
            foreach (int adapter in array)
            {
                long availableRoutes = 0;
                availableRoutes += counts.GetValueOrDefault(adapter - 3);
                availableRoutes += counts.GetValueOrDefault(adapter - 2);
                availableRoutes += counts.GetValueOrDefault(adapter - 1);
                counts[adapter] += availableRoutes;
            }
            long result = counts.GetValueOrDefault(target - 1) + counts.GetValueOrDefault(target - 2) + counts.GetValueOrDefault(target - 3);
            return result;
        }

        const string InputFilePath = "input.txt";

        static void Main(string[] args)
        {
            FileStream fs = null;
            try
            {
                fs = new(InputFilePath, FileMode.Open);
                using (StreamReader reader = new(fs))
                {
                    int[] data = reader.ReadToEnd()
                        .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                        .Select(str => int.Parse(str))
                        .ToArray();
                    Array.Sort(data);
                    int oneJoltGaps = CountGapsOfWidth(data, 1);
                    int threeJoltGaps = CountGapsOfWidth(data, 3) + 1;
                    Console.WriteLine($"Part 1: {oneJoltGaps * threeJoltGaps}");
                    int source = 0;
                    int target = data[^1] + 3;
                    long count = CountDistinctRearrangements(data, source, target);
                    Console.WriteLine($"Part 2: {count}");
                }
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
            }
        }
    }
}
