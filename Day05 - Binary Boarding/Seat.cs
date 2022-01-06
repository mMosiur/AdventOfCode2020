using System;

namespace AdventOfCode.Year2020.Day05;

public struct Seat
{
	public int Row { get; init; }
	public int Column { get; init; }
	public int ID => Row * 8 + Column;

	private static int GetNumber(string Instructions)
	{
		int lower = 0;
		int upper = (1 << Instructions.Length) - 1;
		foreach (char c in Instructions)
		{
			int step = (upper - lower + 1) / 2;
			if (c == 'D')
			{
				upper -= step;
			}
			else if (c == 'U')
			{
				lower += step;
			}
			else throw new FormatException("Invalid instructions format.");
		}
		if (lower != upper) throw new Exception();
		return lower;
	}

	public static Seat FromInstructions(string instructions)
	{
		string rowInstructions = instructions.Substring(0, 7).Replace('F', 'D').Replace('B', 'U');
		string colInstructions = instructions.Substring(7).Replace('L', 'D').Replace('R', 'U');
		int rowNumber = GetNumber(rowInstructions);
		int colNumber = GetNumber(colInstructions);
		return new Seat { Row = rowNumber, Column = colNumber };
	}
}
