using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day03;

public class Day03Solver : DaySolver
{
	private readonly char[,] _map;

	public Day03Solver(string inputFilePath) : base(inputFilePath)
	{
		int width = InputLines.First().Length;
		if (!InputLines.All(line => line.Length == width))
		{
			throw new FormatException("Input is not rectangular.");
		}
		_map = new char[InputLines.Count(), width];
		foreach ((string line, int row) in InputLines.Select((s, i) => (s, i)))
		{
			for (int col = 0; col < width; col++)
			{
				_map[row, col] = line[col];
			}
		}
	}

	private int CountTreeEncounters(int sideSlope, int downSlope)
	{
		int count = 0;
		int i = downSlope;
		int j = sideSlope;
		while (i < _map.GetLength(0))
		{
			char c = _map[i, j];
			if (_map[i, j] == '#') count++;
			i += downSlope;
			j += sideSlope;
			j %= _map.GetLength(1);
		}
		return count;
	}

	public override string SolvePart1()
	{
		int result = CountTreeEncounters(3, 1);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		long result = 1;
		result *= CountTreeEncounters(1, 1);
		result *= CountTreeEncounters(3, 1);
		result *= CountTreeEncounters(5, 1);
		result *= CountTreeEncounters(7, 1);
		result *= CountTreeEncounters(1, 2);
		return result.ToString();
	}
}
