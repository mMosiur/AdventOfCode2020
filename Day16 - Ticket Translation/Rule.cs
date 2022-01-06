using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020.Day16;

public class Rule
{
	public string Name { get; }

	private Tuple<Range, Range> ranges;

	public bool DoesMatch(int number)
	{
		return ranges.Item1.IsInRange(number) || ranges.Item2.IsInRange(number);
	}

	public Rule(string name, Range range1, Range range2)
	{
		Name = name;
		ranges = new Tuple<Range, Range>(range1, range2);
	}
}
