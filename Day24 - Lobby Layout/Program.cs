using System;
using System.IO;
using System.Linq;

namespace Day24
{
    class Program
    {
        static string[] ReadLines(string path)
        {
            FileStream filestream = null;
            try
            {
                filestream = new FileStream(path, FileMode.Open);
                using (StreamReader reader = new StreamReader(filestream))
                {
                    return reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
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
        static void Main(string[] args)
        {
            string[] lines = ReadLines("input.txt");
            HexGrid grid = new HexGrid();
            foreach (string line in lines)
            {
                grid.FlipHexAt(line);
            }
            int blackCount = grid.BlackHexes.Count();
            System.Console.WriteLine($"Part 1: {blackCount}");
            for (int i = 0; i < 100; i++)
            {
                grid.NextDay();
            }
            blackCount = grid.BlackHexes.Count();
            System.Console.WriteLine($"Part 2: {blackCount}");
        }
    }
}
