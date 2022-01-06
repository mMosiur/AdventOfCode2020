using System;
using System.Linq;

namespace AdventOfCode.Year2020.Day02;

public struct OldPasswordPolicy : IPasswordPolicy
{
	public char Letter { get; }
	public int MinCount { get; }
	public int MaxCount { get; }

	public OldPasswordPolicy(char letter, int minCount, int maxCount)
	{
		Letter = letter;
		MinCount = minCount;
		MaxCount = maxCount;
	}

	public bool DoesMatch(string password)
	{
		char search = Letter;
		int count = password.Count(c => c == search);
		if (count < MinCount) return false;
		if (count > MaxCount) return false;
		return true;
	}

	public static OldPasswordPolicy Parse(string s)
	{
		string[] parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		if (parts.Length != 2)
		{
			throw new FormatException($"{nameof(s)} was not in a valid format.");
		}
		if (parts[1].Length != 1)
		{
			throw new FormatException($"{nameof(s)} was not in a valid format.");
		}
		int[] numbers = parts[0].Split('-', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
		if (numbers.Length != 2)
		{
			throw new FormatException($"{nameof(s)} was not in a valid format.");
		}
		return new OldPasswordPolicy(parts[1].Single(), numbers[0], numbers[1]);
	}
}
