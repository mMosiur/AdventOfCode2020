using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day23;

public class Day23Solver : DaySolver
{
	public Day23Solver(string inputFilePath) : base(inputFilePath)
	{
	}

	public OneBasedArray<Cup> GetInput(string str)
	{
		int[] input = str.Select(c => (int)char.GetNumericValue(c)).ToArray();
		OneBasedArray<Cup> cups = new OneBasedArray<Cup>(input.Length);
		for (int i = 1; i < input.Length; i++)
		{
			int num = input[i - 1];
			cups[num] = new Cup(num, input[i]);
		}
		cups[input[^1]] = new Cup(input[^1], input[0]);
		cups.Current = input[0];
		return cups;
	}

	public void MakeMoves(OneBasedArray<Cup> cups, int nofMoves)
	{
		for (int i = 0; i < nofMoves; i++)
		{
			int picked1 = cups[cups.Current].Next;
			int picked2 = cups[picked1].Next;
			int picked3 = cups[picked2].Next;
			cups[cups.Current].Next = cups[picked3].Next;
			int dest = cups.Current - 1;
			if (dest < 1) dest = cups.Length;
			while (dest == picked1 || dest == picked2 || dest == picked3)
			{
				dest--;
				if (dest < 1) dest = cups.Length;
			}
			cups[picked3].Next = cups[dest].Next;
			cups[dest].Next = picked1;
			cups.Current = cups[cups.Current].Next;
		}
	}

	public IEnumerable<Cup> FollowCupArrayFrom(OneBasedArray<Cup> cups, int number)
	{
		int current = number;
		do
		{
			yield return cups[current].Number;
			current = cups[current].Next;
		} while (current != number);
	}

	public OneBasedArray<Cup> GetInput(string str, int desiredSize)
	{
		int[] input = str.Select(c => (int)char.GetNumericValue(c)).ToArray();
		OneBasedArray<Cup> cups = new OneBasedArray<Cup>(desiredSize);
		for (int i = 1; i < input.Length; i++)
		{
			int num = input[i - 1];
			cups[num] = new Cup(num, input[i]);
		}
		cups[input[^1]] = new Cup(input[^1], input.Length + 1);
		for (int i = input.Length + 2; i <= desiredSize; i++)
		{
			int num = i - 1;
			cups[num] = new Cup(num, i);
		}
		cups[desiredSize] = new Cup(desiredSize, input[0]);
		cups.Current = input[0];
		return cups;
	}

	public override string SolvePart1()
	{
		const int NumberOfCups = 100;
		OneBasedArray<Cup> cups = GetInput(Input.Trim());
		MakeMoves(cups, NumberOfCups);
		var cupsAfter1 = FollowCupArrayFrom(cups, 1).Skip(1);
		string result = string.Concat(cupsAfter1);
		return result;
	}

	public override string SolvePart2()
	{
		const int NumberOfCups = 1_000_000;
		const int NumberOfMoves = 10_000_000;
		OneBasedArray<Cup> cups = GetInput(Input.Trim(), NumberOfCups);
		MakeMoves(cups, NumberOfMoves);
		long part2 = FollowCupArrayFrom(cups, 1)
			.Skip(1).Take(2) // skip the cup number 1 and consider only two after it
			.Aggregate(1L, (acc, next) => acc * next.Number);
		return part2.ToString();
	}
}
