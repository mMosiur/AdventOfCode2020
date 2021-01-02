using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Program
    {

        static int NextInSequence(List<int> seq)
        {
            int last = seq[^1];
            int pos = seq.Count - 2;
            while (pos >= 0 && seq[pos] != last) pos--;
            if (pos < 0) return 0;
            return seq.Count - 1 - pos;

        }

        static int AddToSequenceAndGetNext(Dictionary<int, int> seq, int index, int current)
        {
            // [0,3,6], 0
            int pos = 0;
            bool isRepeat = seq.TryGetValue(current, out pos);
            seq[current] = index;
            return isRepeat ? index - pos : 0;
        }
        const string InputFilePath = "input.txt";



        static void Main(string[] args)
        {
            FileStream filestream = null;
            try
            {
                filestream = new(InputFilePath, FileMode.Open);
                using (StreamReader reader = new(filestream))
                {
                    List<int> nums = reader.ReadToEnd().Split(',').Select(str => int.Parse(str)).ToList();
                    // Part 1
                    Dictionary<int, int> sequence = new();
                    for (int i = 0; i < nums.Count - 1; i++)
                        sequence[nums[i]] = i;
                    int current = nums[^1];
                    for (int i = nums.Count - 1; i < (2020 - 1); i++)
                    {
                        current = AddToSequenceAndGetNext(sequence, i, current);
                    }
                    System.Console.WriteLine($"Part 1: {current}");
                    // Part 2
                    sequence = new();
                    for (int i = 0; i < nums.Count - 1; i++)
                        sequence[nums[i]] = i;
                    current = nums[^1];
                    for (int i = nums.Count - 1; i < (30000000 - 1); i++)
                    {
                        current = AddToSequenceAndGetNext(sequence, i, current);
                    }
                    System.Console.WriteLine($"Part 1: {current}");
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
    }
}
