using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day04;

public class Day04Solver : DaySolver
{
	private readonly ICollection<Passport> _passports;

	public Day04Solver(string inputFilePath) : base(inputFilePath)
	{
		_passports = new List<Passport>();
		using IEnumerator<string> it = InputLines.GetEnumerator();
		Passport passport = new Passport();
		while (it.MoveNext())
		{
			if (it.Current.Equals(""))
			{
				_passports.Add(passport);
				passport = new Passport();
			}
			foreach (string field in it.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				passport.AddPassportField(field);
			}
		}
		if (passport.FieldCount > 0)
		{
			_passports.Add(passport);
		}
	}

	public override string SolvePart1()
	{
		int result = _passports.Count(p => p.IsValid);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		int result = _passports.Count(p => p.IsStrictlyValid);
		return result.ToString();
	}
}
