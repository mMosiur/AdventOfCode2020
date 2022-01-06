using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day05;

public class Day05Solver : DaySolver
{
	private ICollection<Seat> _seats;

	public Day05Solver(string inputFilePath) : base(inputFilePath)
	{
		_seats = InputLines.Select(Seat.FromInstructions).ToList();
	}

	public override string SolvePart1()
	{
		int result = _seats.Max(seat => seat.ID);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		const int RowCount = 128;
		const int ColumnCount = 8;
		bool[,] seats = new bool[RowCount, ColumnCount];
		foreach (Seat seat in _seats)
		{
			seats[seat.Row, seat.Column] = true;
		}
		bool foundOne = false;
		for (int row = 0; row < RowCount; row++)
		{
			for (int col = 0; col < ColumnCount; col++)
			{
				if (seats[row, col])
				{
					foundOne = true;
					continue;
				}
				if (!foundOne) continue;
				return (row * ColumnCount + col).ToString();
			}
		}
		throw new Exception("My seat was not found!");
	}
}
