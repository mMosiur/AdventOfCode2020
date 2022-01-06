using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day02;

public class Day02Solver : DaySolver
{
	IEnumerable<(string PolicyString, string Password)> _passwords;
	public Day02Solver(string inputFilePath) : base(inputFilePath)
	{
		_passwords = InputLines.Select(line =>
		{
			string[] parts = line.Split(':', StringSplitOptions.TrimEntries);
			if (parts.Length != 2)
			{
				throw new FormatException($"{nameof(line)} was not in a valid format.");
			}
			return (parts[0], parts[1]);
		});
	}

	public override string SolvePart1()
	{
		return _passwords
			.Select(el => (OldPasswordPolicy.Parse(el.PolicyString), el.Password))
			.Where(el => el.Item1.DoesMatch(el.Item2))
			.Count()
			.ToString();
	}

	public override string SolvePart2()
	{
		return _passwords
			.Select(el => (NewPasswordPolicy.Parse(el.PolicyString), el.Password))
			.Where(el => el.Item1.DoesMatch(el.Item2))
			.Count()
			.ToString();
	}
}
