using System;
using System.Linq;

namespace AdventOfCode.Year2020.Day16;

public record Range
{
	public int Start { get; }
	public int End { get; }

	public Range(string str)
	{
		string[] parts = str.Trim().Split('-').Select(part => part.Trim("()".ToCharArray())).ToArray();
		if (parts.Length != 2) throw new ArgumentException();
		Start = int.Parse(parts[0]);
		End = int.Parse(parts[1]);
	}

	public bool IsInRange(int number)
	{
		return Start <= number && number <= End;
	}

	public override string ToString()
	{
		string startStr = Start.ToString();
		if (Start < 0) startStr = $"({startStr})";
		string endStr = End.ToString();
		if (End < 0) endStr = $"({endStr})";
		return $"{startStr}-{endStr}";
	}
}
