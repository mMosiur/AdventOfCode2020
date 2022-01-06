using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day24;

public class Day24Solver : DaySolver
{
	private bool _flipped = false;
	private readonly HexGrid _grid = new();

	public Day24Solver(string inputFilePath) : base(inputFilePath)
	{
	}

	private void FlipIfNotFlipped(IEnumerable<string> flipPaths)
	{
		if (_flipped) return;
		foreach (string path in flipPaths)
		{
			_grid.FlipHexAt(path);
		}
		_flipped = true;
	}

	public override string SolvePart1()
	{
		FlipIfNotFlipped(InputLines);
		int result = _grid.BlackHexes.Count();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		FlipIfNotFlipped(InputLines);
		const int Iterations = 100;
		for (int i = 0; i < Iterations; i++)
		{
			_grid.NextDay();
		}
		int result = _grid.BlackHexes.Count();
		return result.ToString();
	}
}
