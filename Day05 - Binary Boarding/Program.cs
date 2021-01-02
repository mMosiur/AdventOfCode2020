using System;
using System.IO;

namespace Day05
{
    struct Seat
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int ID => Row * 8 + Col;
    }

    class Program
    {
        static int Span(int a, int b)
        {
            return b - a + 1;
        }

        static int GetNumber(string Instructions)
        {
            int lower = 0;
            int upper = (1 << Instructions.Length) - 1;
            foreach (char c in Instructions)
            {
                int step = Span(lower, upper) / 2;
                if (c == 'D')
                {
                    upper -= step;
                }
                else if (c == 'U')
                {
                    lower += step;
                }
            }
            if (lower != upper) throw new Exception();
            return lower;
        }

        static Seat GetSeat(string Instructions)
        {
            string rowInstructions = Instructions.Substring(0, 7).Replace('F', 'D').Replace('B', 'U');
            string colInstructions = Instructions.Substring(7).Replace('L', 'D').Replace('R', 'U');
            int rowNumber = GetNumber(rowInstructions);
            int colNumber = GetNumber(colInstructions);
            return new Seat { Row = rowNumber, Col = colNumber };
        }

        static int GetMaxID()
        {
            int max = 0;
            using (FileStream fs = new FileStream("input.txt", FileMode.Open))
            {
                StreamReader reader = new StreamReader(fs);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Seat seat = GetSeat(line);
                    if (seat.ID > max) max = seat.ID;
                }
            }
            return max;
        }

        static int GetMySeatID()
        {
            bool[,] seats = new bool[128, 8];
            using (FileStream fs = new FileStream("input.txt", FileMode.Open))
            {
                StreamReader reader = new StreamReader(fs);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Seat seat = GetSeat(line);
                    seats[seat.Row, seat.Col] = true;
                }
            }
            bool foundOne = false;
            for (int row = 0; row < 128; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (seats[row, col])
                    {
                        foundOne = true;
                        continue;
                    }
                    if(!foundOne) continue;
                    return row * 8 + col;
                }
            }
            return -1;
        }

        static void Main(string[] args)
        {
            System.Console.WriteLine($"Max seat ID: {GetMaxID()}");
            System.Console.WriteLine($"My seat ID: {GetMySeatID()}");
        }
    }
}
