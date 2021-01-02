using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    class Program
    {
        static bool IsSumOfTwo(long number, LinkedList<long> array)
        {
            for (LinkedListNode<long> node1 = array.First; node1 != null; node1 = node1.Next)
            {
                for (LinkedListNode<long> node2 = node1.Next; node2 != null; node2 = node2.Next)
                {
                    if (node1.Value + node2.Value == number)
                        return true;
                }
            }
            return false;
        }

        static long GetFirstInvalid(long[] data)
        {
            LinkedList<long> list = new LinkedList<long>(data.Take(25));
            for (int i = 25; i < data.Length; i++)
            {
                if (!IsSumOfTwo(data[i], list))
                    return data[i];
                list.RemoveFirst();
                list.AddLast(data[i]);
            }
            return -1;
        }

        static long[] GetRangeThatSumTo(long[] data, long number)
        {
            int left = 0;
            int right = 0;
            long sum = data[0];
            while (left <= right && right < data.Length)
            {
                if (sum < number)
                {
                    right++;
                    sum += data[right];
                }
                else if (sum > number)
                {
                    sum -= data[left];
                    left++;
                }
                else
                {
                    long[] result = new long[right - left + 1];
                    for (int i = left; i <= right; i++)
                    {
                        result[i-left] = data[i];
                    }
                    return result;
                }
            }
            return null;
        }

        static void Main(string[] args)
        {
            FileStream fileStream = new("input.txt", FileMode.Open);
            using (StreamReader reader = new(fileStream))
            {
                var data = reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(str => long.Parse(str)).ToArray();
                long invalid = GetFirstInvalid(data);
                Console.WriteLine($"First invalid number: {invalid}");
                long[] range = GetRangeThatSumTo(data, invalid);
                long weakness = range.Min() + range.Max();
                Console.WriteLine($"Encryption weakness: {weakness}");
            }
        }
    }
}
