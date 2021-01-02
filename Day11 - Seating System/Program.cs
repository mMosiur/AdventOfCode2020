using System;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        static char Get(char[,] map, int index1, int index2)
        {
            if (index1 < 0 || index1 >= map.GetLength(0)) return (char)0;
            if (index2 < 0 || index2 >= map.GetLength(1)) return (char)0;
            return map[index1, index2];
        }

        static int CountOccupiedNeighbors(char[,] map, int index1, int index2)
        {
            int count = 0;
            if (Get(map, index1 - 1, index2 - 1) == '#') count++;
            if (Get(map, index1 - 1, index2) == '#') count++;
            if (Get(map, index1 - 1, index2 + 1) == '#') count++;
            if (Get(map, index1, index2 - 1) == '#') count++;
            if (Get(map, index1, index2 + 1) == '#') count++;
            if (Get(map, index1 + 1, index2 - 1) == '#') count++;
            if (Get(map, index1 + 1, index2) == '#') count++;
            if (Get(map, index1 + 1, index2 + 1) == '#') count++;
            return count;
        }

        static char[,] AdvanceMap(char[,] map, out bool changed)
        {
            changed = false;
            char[,] result = (char[,])map.Clone();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    int occupiedNeighborsCount = CountOccupiedNeighbors(map, i, j);
                    switch (map[i,j])
                    {
                        case 'L':
                            if (occupiedNeighborsCount == 0)
                            {
                                changed = true;
                                result[i, j] = '#';
                            }
                            break;
                        case '#':
                            if (occupiedNeighborsCount >= 4)
                            {
                                result[i, j] = 'L';
                                changed = true;
                            }
                            break;
                    }
                }
            }
            return result;
        }

        static int CountOccupiedSeats(char[,] map)
        {
            int count = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if(map[i,j] == '#') count++;
                }
            }
            return count;
        }

        static bool IsSeatOccupiedInDirection(char[,] map, int index1, int index2, int dir1, int dir2)
        {
            index1 += dir1;
            index2 += dir2;
            char seat = Get(map, index1, index2);
            while (seat != (char)0)
            {
                if (seat == '#') return true;
                if (seat == 'L') return false;
                index1 += dir1;
                index2 += dir2;
                seat = Get(map, index1, index2);
            }
            return false;
        }

        static int CountOccupiedVisibleSeats(char[,] map, int index1, int index2)
        {
            int count = 0;
            if (IsSeatOccupiedInDirection(map, index1, index2, -1, -1)) count++;
            if (IsSeatOccupiedInDirection(map, index1, index2, -1, 0)) count++;
            if (IsSeatOccupiedInDirection(map, index1, index2, -1, 1)) count++;
            if (IsSeatOccupiedInDirection(map, index1, index2, 0, -1)) count++;
            if (IsSeatOccupiedInDirection(map, index1, index2, 0, 1)) count++;
            if (IsSeatOccupiedInDirection(map, index1, index2, 1, -1)) count++;
            if (IsSeatOccupiedInDirection(map, index1, index2, 1, 0)) count++;
            if (IsSeatOccupiedInDirection(map, index1, index2, 1, 1)) count++;
            return count;
        }

        static char[,] AdvanceMapAlternative(char[,] map, out bool changed)
        {
            changed = false;
            char[,] result = (char[,])map.Clone();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    int occupiedNeighborsCount = CountOccupiedVisibleSeats(map, i, j);
                    switch (map[i,j])
                    {
                        case 'L':
                            if (occupiedNeighborsCount == 0)
                            {
                                changed = true;
                                result[i, j] = '#';
                            }
                            break;
                        case '#':
                            if (occupiedNeighborsCount >= 5)
                            {
                                result[i, j] = 'L';
                                changed = true;
                            }
                            break;
                    }
                }
            }
            return result;
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
                    string[] lines = reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    char[,] map = new char[lines.Length, lines[0].Length];
                    for (int i = 0; i < lines.Length; i++)
                    {
                        for (int j = 0; j < lines[i].Length; j++)
                        {
                            map[i, j] = lines[i][j];
                        }
                    }
                    bool changed = true;
                    char[,] map1 = (char[,])map.Clone();
                    while (changed)
                    {
                        map1 = AdvanceMap(map1, out changed);
                    }
                    System.Console.WriteLine($"Occupied seats: {CountOccupiedSeats(map1)}");
                    char[,] map2 = (char[,])map.Clone();
                    changed = true;
                    while (changed)
                    {
                        map2 = AdvanceMapAlternative(map2, out changed);
                    }
                    System.Console.WriteLine($"Alternative occupied seats: {CountOccupiedSeats(map2)}");
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
