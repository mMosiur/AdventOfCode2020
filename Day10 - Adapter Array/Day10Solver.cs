using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day10;

public class Day10Solver : DaySolver
{
	private int[] _data;

	public Day10Solver(string inputFilePath) : base(inputFilePath)
	{
		_data = InputLines.Select(int.Parse).ToArray();
		Array.Sort(_data);
	}


	private int CountGapsOfWidth(int[] data, int width)
	{
		int count = 0;
		if (data[0] == width) count++;
		for (int i = 1; i < data.Length; i++)
		{
			if (data[i] - data[i - 1] == width)
				count++;
		}
		return count;
	}

	private long CountDistinctRearrangements(int[] array, int source, int target)
	{
		SortedDictionary<int, long> counts = new();
		counts.Add(source, 1);
		foreach (int el in array)
			counts.Add(el, 0);
		foreach (int adapter in array)
		{
			long availableRoutes = 0;
			availableRoutes += counts.GetValueOrDefault(adapter - 3);
			availableRoutes += counts.GetValueOrDefault(adapter - 2);
			availableRoutes += counts.GetValueOrDefault(adapter - 1);
			counts[adapter] += availableRoutes;
		}
		long result = counts.GetValueOrDefault(target - 1) + counts.GetValueOrDefault(target - 2) + counts.GetValueOrDefault(target - 3);
		return result;
	}

	public override string SolvePart1()
	{
		int oneJoltGaps = CountGapsOfWidth(_data, 1);
		int threeJoltGaps = CountGapsOfWidth(_data, 3) + 1;
		int result = oneJoltGaps * threeJoltGaps;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		int source = 0;
		int target = _data[^1] + 3;
		long result = CountDistinctRearrangements(_data, source, target);
		return result.ToString();
	}
}
