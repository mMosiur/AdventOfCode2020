using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day20
{
    class Program
    {
        static char[,] LinesToCharArray(string[] lines)
        {
            int len = lines[0].Length;
            if (!lines.All(line => line.Length == len))
                throw new Exception();
            char[,] array = new char[lines.Length, len];
            for (int r = 0; r < lines.Length; r++)
            {
                for (int c = 0; c < len; c++)
                {
                    array[r, c] = lines[r][c];
                }
            }
            return array;
        }

        static List<Tile> ReadInput(string path)
        {
            List<Tile> list = new List<Tile>();
            FileStream filestream = null;
            try
            {
                filestream = new(path, FileMode.Open);
                using (StreamReader reader = new(filestream))
                {
                    foreach (string tileStr in reader.ReadToEnd().Split("\n\n", StringSplitOptions.RemoveEmptyEntries))
                    {
                        StringReader stringReader = new StringReader(tileStr);
                        string line = stringReader.ReadLine();
                        int id = int.Parse(line.Substring(5, line.Length - 6));
                        string[] lines = stringReader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
                        list.Add(new Tile(id, LinesToCharArray(lines)));
                    }
                }
            }
            finally
            {
                if (filestream != null)
                {
                    filestream.Dispose();
                }
            }
            return list;
        }

        static void Main(string[] args)
        {
            List<Tile> tiles = ReadInput("input.txt");
            double sqrt = Math.Sqrt(tiles.Count);
            int side = Convert.ToInt32(sqrt);
            if (!sqrt.Equals(side))
                throw new Exception("Tiles cannot be arranged into squares");
            Dictionary<int, List<Tile>> edges = new Dictionary<int, List<Tile>>();
            foreach (Tile tile in tiles)
            {
                foreach (var edge in tile.Edges)
                {
                    int hash = Tile.GetEdgeHash(edge);
                    if (edges.ContainsKey(hash))
                        edges[hash].Add(tile);
                    else edges[hash] = new List<Tile>() { tile };
                }
            }
            TileGrid grid = new TileGrid(side, side);
            Tile corner = tiles.First(tile => tile.EdgeHashes.Count(hash => edges[hash].Count == 1) == 2);
            grid[0, 0] = corner;
            foreach (Tile rotation in grid[0, 0].Rotations)
            {
                if (edges[rotation.TopEdgeHash].Count != 1) continue;
                if (edges[rotation.LeftEdgeHash].Count != 1) continue;
                break;
            }
            tiles.Remove(grid[0, 0]);
            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    if (grid[r, c] != null) continue;
                    foreach (Tile tile in tiles)
                    {
                        bool inserted = grid.InsertAt(r, c, tile);
                        if (inserted) break;
                    }
                    tiles.Remove(grid[r, c]);
                }
            }
            long idProduct = (long)grid[0, 0].ID * grid[0, ^1].ID * grid[^1, 0].ID * grid[^1, ^1].ID;
            Console.WriteLine($"Part 1: {idProduct}");
            Image image = grid.Merge();
            //Console.WriteLine(image);
            int count = 0;
            foreach (Image rotation in image.Rotations)
            {
                count = rotation.CountSeaMonsters();
                if(count > 0) break;
            }
            int part2 = image.Count(c => c == '#');
            part2 -= count * image.SeaMonsterSize;
            System.Console.WriteLine($"Part 2: {part2}");
        }
    }
}
