using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day20;

public class Day20Solver : DaySolver
{
	private IList<Tile> _tiles;

	private Lazy<TileGrid> _grid;

	public Day20Solver(string inputFilePath) : base(inputFilePath)
	{
		_tiles = new List<Tile>();
		foreach (string tileStr in Input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries))
		{
			StringReader stringReader = new StringReader(tileStr);
			string line = stringReader.ReadLine() ?? throw new FormatException();
			int id = int.Parse(line.Substring(5, line.Length - 6));
			string[] lines = stringReader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
			_tiles.Add(new Tile(id, LinesToCharArray(lines)));
		}
		_grid = new Lazy<TileGrid>(AssembleGrid);
	}

	private TileGrid AssembleGrid()
	{
		double sqrt = Math.Sqrt(_tiles.Count);
		int side = Convert.ToInt32(sqrt);
		if (!sqrt.Equals(side))
			throw new Exception("Tiles cannot be arranged into squares");
		Dictionary<int, List<Tile>> edges = new Dictionary<int, List<Tile>>();
		foreach (Tile tile in _tiles)
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
		Tile corner = _tiles.First(tile => tile.EdgeHashes.Count(hash => edges[hash].Count == 1) == 2);
		grid[0, 0] = corner;
		foreach (Tile rotation in corner.Rotations)
		{
			if (edges[rotation.TopEdgeHash].Count != 1) continue;
			if (edges[rotation.LeftEdgeHash].Count != 1) continue;
			break;
		}
		_tiles.Remove(corner);
		for (int r = 0; r < grid.GetLength(0); r++)
		{
			for (int c = 0; c < grid.GetLength(1); c++)
			{
				if (grid[r, c] != null) continue;
				foreach (Tile tile in _tiles)
				{
					bool inserted = grid.InsertAt(r, c, tile);
					if (inserted) break;
				}
				_tiles.Remove(corner);
			}
		}
		return grid;
	}


	private char[,] LinesToCharArray(string[] lines)
	{
		int len = lines[0].Length;
		if (!lines.All(line => line.Length == len))
			throw new FormatException();
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

	public override string SolvePart1()
	{
		long idProduct = _grid.Value[0, 0]?.ID ?? throw new InvalidOperationException();
		idProduct *= _grid.Value[0, ^1]?.ID ?? throw new InvalidOperationException();
		idProduct *= _grid.Value[^1, 0]?.ID ?? throw new InvalidOperationException();
		idProduct *= _grid.Value[^1, ^1]?.ID ?? throw new InvalidOperationException();
		return idProduct.ToString();
	}

	public override string SolvePart2()
	{
		Image image = _grid.Value.Merge();
		int count = 0;
		foreach (Image rotation in image.Rotations)
		{
			count = rotation.CountSeaMonsters();
			if (count > 0) break;
		}
		int result = image.Count(c => c == '#');
		result -= count * image.SeaMonsterSize;
		return result.ToString();
	}
}
