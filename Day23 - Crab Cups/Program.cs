using System;
using System.Collections.Generic;
using System.Linq;

namespace Day23
{
    class Program
    {

        static int CharToInt(char c)
        {
            if (!Char.IsDigit(c))
                throw new FormatException();
            return (int)c - (int)'0';
        }

        static OneBasedArray<Cup> GetInput(string str)
        {
            int[] input = str.Select(c => CharToInt(c)).ToArray();
            OneBasedArray<Cup> cups = new OneBasedArray<Cup>(input.Length);
            for (int i = 1; i < input.Length; i++)
            {
                int num = input[i - 1];
                cups[num] = new Cup(num, input[i]);
            }
            cups[input[^1]] = new Cup(input[^1], input[0]);
            cups.Current = input[0];
            return cups;
        }

        static OneBasedArray<Cup> GetInput(string str, int desiredSize)
        {
            int[] input = str.Select(c => CharToInt(c)).ToArray();
            OneBasedArray<Cup> cups = new OneBasedArray<Cup>(desiredSize);
            for (int i = 1; i < input.Length; i++)
            {
                int num = input[i - 1];
                cups[num] = new Cup(num, input[i]);
            }
            cups[input[^1]] = new Cup(input[^1], input.Length+1);
            for (int i = input.Length + 2; i <= desiredSize; i++)
            {
                int num = i - 1;
                cups[num] = new Cup(num, i);
            }
            cups[desiredSize] = new Cup(desiredSize, input[0]);
            cups.Current = input[0];
            return cups;
        }

        static IEnumerable<Cup> FollowCupArrayFrom(OneBasedArray<Cup> cups, int number)
        {
            int current = number;
            do
            {
                yield return cups[current].Number;
                current = cups[current].Next;
            } while (current != number);
        }

        static void MakeMoves(OneBasedArray<Cup> cups, int nofMoves)
        {
            for (int i = 0; i < nofMoves; i++)
            {
                int picked1 = cups[cups.Current].Next;
                int picked2 = cups[picked1].Next;
                int picked3 = cups[picked2].Next;
                cups[cups.Current].Next = cups[picked3].Next;
                int dest = cups.Current - 1;
                if (dest < 1) dest = cups.Length;
                while (dest == picked1 || dest == picked2 || dest == picked3)
                {
                    dest--;
                    if (dest < 1) dest = cups.Length;
                }
                cups[picked3].Next = cups[dest].Next;
                cups[dest].Next = picked1;
                cups.Current = cups[cups.Current].Next;
            }
        }

        const string Test = "389125467";
        const string Input = "137826495";

        static void Main(string[] args)
        {
            string data = Input;
            OneBasedArray<Cup> cups = GetInput(data);
            MakeMoves(cups, 100);
            Console.WriteLine($"Part 1: {string.Join("", FollowCupArrayFrom(cups, 1).Skip(1))}");
            cups = GetInput(data, 1000000);
            MakeMoves(cups, 10000000);
            long part2 = 1;
            foreach(Cup cup in FollowCupArrayFrom(cups, 1).Skip(1).Take(2))
                part2 *= cup.Number;
            Console.WriteLine($"Part 2: {part2}");
        }
    }
}
