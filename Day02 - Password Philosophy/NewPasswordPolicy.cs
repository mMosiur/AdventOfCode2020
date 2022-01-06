using System;
using System.Linq;

namespace AdventOfCode.Year2020.Day02;

public struct NewPasswordPolicy : IPasswordPolicy
{
	public char Letter { get; }
	public int FirstPos { get; }
	public int SecondPos { get; }

	public NewPasswordPolicy(char letter, int firstPos, int secondPos)
	{
		Letter = letter;
		FirstPos = firstPos;
		SecondPos = secondPos;
	}

	public bool DoesMatch(string password)
	{
		if (password.Length < FirstPos) return false;
		int count = 0;
		if (password[FirstPos - 1] == Letter) count++;
		if (SecondPos <= password.Length)
		{
			if (password[SecondPos - 1] == Letter) count++;
		}
		return count == 1;
	}

	public static NewPasswordPolicy Parse(string s)
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
		return new NewPasswordPolicy(parts[1].Single(), numbers[0], numbers[1]);
	}
}
