using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    class Program
    {
        static int[] GetData()
        {
            using (StreamReader stream = new("input.txt"))
            {
                var input = stream.ReadToEnd().Split('\n').Where(str=>str!="").Select(str => int.Parse(str));
                return input.ToArray();
            }
        }

        static int TwoNumSum(IEnumerable<int> data, int expectedSum)
        {
            HashSet<int> nums = new();
            foreach (int num in data)
            {
                int complement = expectedSum - num;
                if (nums.Contains(complement))
                {
                    return num * complement;
                }
                else
                {
                    nums.Add(num);
                }
            }
            return -1;
        }

        static int ThreeNumSum(IEnumerable<int> data, int expectedSum)
        {
            foreach(int num in data)
            {
                int complement = expectedSum - num;
                int result = TwoNumSum(data, complement);
                if(result == -1) continue;
                return result * num;
            }
            return -1;
        }

        static void Main(string[] args)
        {
            var data = GetData();
            int result1 = TwoNumSum(data, 2020);
            Console.WriteLine($"Result 1: {result1}");
            int result2 = ThreeNumSum(data, 2020);
            Console.WriteLine($"Result 2: {result2}");
        }
    }
}
