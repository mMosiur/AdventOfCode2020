using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day09;

public class Day09Solver : DaySolver
{
	private readonly long[] _data;
	private long? _part1;

	public Day09Solver(string inputFilePath) : base(inputFilePath)
	{
		_data = InputLines.Select(long.Parse).ToArray();
	}

	private bool IsSumOfTwo(long number, LinkedList<long> array)
	{
		for (LinkedListNode<long>? node1 = array.First; node1 is not null; node1 = node1.Next)
		{
			for (LinkedListNode<long>? node2 = node1.Next; node2 != null; node2 = node2.Next)
			{
				if (node1.Value + node2.Value == number)
					return true;
			}
		}
		return false;
	}

	private long GetFirstInvalid(long[] data)
	{
		LinkedList<long> list = new LinkedList<long>(data.Take(25));
		for (int i = 25; i < data.Length; i++)
		{
			if (!IsSumOfTwo(data[i], list))
				return data[i];
			list.RemoveFirst();
			list.AddLast(data[i]);
		}
		return -1;
	}

	long[] GetRangeThatSumTo(long[] data, long number)
	{
		int left = 0;
		int right = 0;
		long sum = data[0];
		while (left <= right && right < data.Length)
		{
			if (sum < number)
			{
				right++;
				sum += data[right];
			}
			else if (sum > number)
			{
				sum -= data[left];
				left++;
			}
			else
			{
				long[] result = new long[right - left + 1];
				for (int i = left; i <= right; i++)
				{
					result[i - left] = data[i];
				}
				return result;
			}
		}
		throw new ArgumentException("No solution found for given arguments.");
	}

	public override string SolvePart1()
	{
		if (!_part1.HasValue)
		{
			_part1 = GetFirstInvalid(_data);
		}
		return _part1.Value.ToString();
	}

	public override string SolvePart2()
	{
		if (!_part1.HasValue)
		{
			_part1 = GetFirstInvalid(_data);
		}
		long[] range = GetRangeThatSumTo(_data, _part1.Value);
		long weakness = range.Min() + range.Max();
		return weakness.ToString();
	}
}
