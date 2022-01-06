using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day01;

public class Day01Solver : DaySolver
{
	private ICollection<int> _numbers;

	public Day01Solver(string inputFilePath) : base(inputFilePath)
	{
		_numbers = InputLines.Select(int.Parse).ToList();
	}

	private (int, int) GetTwoNumbersThatSumTo(int expectedSum)
	{
		HashSet<int> numberSet = new();
		foreach (int number in _numbers)
		{
			int complement = expectedSum - number;
			if (numberSet.Contains(complement))
			{
				return (complement, number);
			}
			else
			{
				numberSet.Add(number);
			}
		}
		throw new InvalidOperationException($"No two numbers found that sum to {expectedSum}.");
	}

	private (int, int, int) GetThreeNumbersThatSumTo(int expectedSum)
	{
		foreach (int number in _numbers)
		{
			int complement = expectedSum - number;
			try
			{
				(int number1, int number2) = GetTwoNumbersThatSumTo(complement);
				return (number1, number2, number);
			}
			catch (InvalidOperationException) { }
		}
		throw new InvalidOperationException($"No three numbers found that sum to {expectedSum}.");
	}

	public override string SolvePart1()
	{
		const int expectedSum = 2020;
		(int number1, int number2) = GetTwoNumbersThatSumTo(expectedSum);
		int result = number1 * number2;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		const int expectedSum = 2020;
		(int number1, int number2, int number3) = GetThreeNumbersThatSumTo(expectedSum);
		int result = number1 * number2 * number3;
		return result.ToString();
	}
}
