using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day15;

public class Day15Solver : DaySolver
{
	private readonly List<int> _numbers;

	public Day15Solver(string inputFilePath) : base(inputFilePath)
	{
		_numbers = InputLines.Single().Split(',').Select(int.Parse).ToList();
	}

	private int NextInSequence(List<int> seq)
	{
		int last = seq[^1];
		int pos = seq.Count - 2;
		while (pos >= 0 && seq[pos] != last) pos--;
		if (pos < 0) return 0;
		return seq.Count - 1 - pos;
	}

	private int AddToSequenceAndGetNext(Dictionary<int, int> seq, int index, int current)
	{
		int pos = 0;
		bool isRepeat = seq.TryGetValue(current, out pos);
		seq[current] = index;
		return isRepeat ? index - pos : 0;
	}

	public override string SolvePart1()
	{
		Dictionary<int, int> sequence = new();
		for (int i = 0; i < _numbers.Count - 1; i++)
			sequence[_numbers[i]] = i;
		int current = _numbers[^1];
		for (int i = _numbers.Count - 1; i < (2020 - 1); i++)
		{
			current = AddToSequenceAndGetNext(sequence, i, current);
		}
		return current.ToString();
	}

	public override string SolvePart2()
	{
		Dictionary<int, int> sequence = new();
		for (int i = 0; i < _numbers.Count - 1; i++)
			sequence[_numbers[i]] = i;
		int current = _numbers[^1];
		for (int i = _numbers.Count - 1; i < (30000000 - 1); i++)
		{
			current = AddToSequenceAndGetNext(sequence, i, current);
		}
		return current.ToString();
	}
}
