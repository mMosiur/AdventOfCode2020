using System.Collections.Generic;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day06;

public class Day06Solver : DaySolver
{
	private List<List<string>> _groups;

	public Day06Solver(string inputFilePath) : base(inputFilePath)
	{
		_groups = new List<List<string>>();
		List<string> group = new List<string>();
		IEnumerator<string> it = InputLines.GetEnumerator();
		while (it.MoveNext())
		{
			if (it.Current.Equals(""))
			{
				if (group.Count > 0)
				{
					_groups.Add(group);
					group = new List<string>();
				}
			}
			else
			{
				group.Add(it.Current);
			}
		}
		if (group.Count > 0)
		{
			_groups.Add(group);
		}
	}

	public override string SolvePart1()
	{
		int sum = 0;
		foreach (IEnumerable<string> group in _groups)
		{
			HashSet<char> chars = new();
			foreach (string answers in group)
				foreach (char c in answers)
					chars.Add(c);
			sum += chars.Count;
		}
		return sum.ToString();
	}

	public override string SolvePart2()
	{
		int sum = 0;
		foreach (IEnumerable<string> group in _groups)
		{
			HashSet<char> chars = new("abcdefghijklmnopqrstuvwxyz");
			foreach (string answers in group)
				chars.IntersectWith(answers);
			sum += chars.Count;
		}
		return sum.ToString();
	}
}
