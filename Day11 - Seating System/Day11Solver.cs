using System.IO;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day11;

public class Day11Solver : DaySolver
{
	private readonly char[,] _map;

	public Day11Solver(string inputFilePath) : base(inputFilePath)
	{
		string[] lines = InputLines.ToArray();
		int height = lines.Length;
		int width = lines[0].Length;
		if (!lines.All(line => line.Length == width))
		{
			throw new InvalidDataException("Input is not rectangular.");
		}
		_map = new char[height, width];
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				_map[i, j] = lines[i][j];
			}
		}
	}

	private char Get(char[,] map, int index1, int index2)
	{
		if (index1 < 0 || index1 >= map.GetLength(0)) return (char)0;
		if (index2 < 0 || index2 >= map.GetLength(1)) return (char)0;
		return map[index1, index2];
	}

	private int CountOccupiedNeighbors(char[,] map, int index1, int index2)
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

	char[,] AdvanceMap(char[,] map, out bool changed)
	{
		changed = false;
		char[,] result = (char[,])map.Clone();
		for (int i = 0; i < map.GetLength(0); i++)
		{
			for (int j = 0; j < map.GetLength(1); j++)
			{
				int occupiedNeighborsCount = CountOccupiedNeighbors(map, i, j);
				switch (map[i, j])
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

	int CountOccupiedSeats(char[,] map)
	{
		int count = 0;
		for (int i = 0; i < map.GetLength(0); i++)
		{
			for (int j = 0; j < map.GetLength(1); j++)
			{
				if (map[i, j] == '#') count++;
			}
		}
		return count;
	}

	bool IsSeatOccupiedInDirection(char[,] map, int index1, int index2, int dir1, int dir2)
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

	int CountOccupiedVisibleSeats(char[,] map, int index1, int index2)
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

	char[,] AdvanceMapAlternative(char[,] map, out bool changed)
	{
		changed = false;
		char[,] result = (char[,])map.Clone();
		for (int i = 0; i < map.GetLength(0); i++)
		{
			for (int j = 0; j < map.GetLength(1); j++)
			{
				int occupiedNeighborsCount = CountOccupiedVisibleSeats(map, i, j);
				switch (map[i, j])
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

	public override string SolvePart1()
	{
		char[,] map = (char[,])_map.Clone();
		bool changed = true;
		while (changed)
		{
			map = AdvanceMap(map, out changed);
		}
		int result = CountOccupiedSeats(map);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		char[,] map = (char[,])_map.Clone();
		bool changed = true;
		while (changed)
		{
			map = AdvanceMapAlternative(map, out changed);
		}
		int result = CountOccupiedSeats(map);
		return result.ToString();
	}
}
