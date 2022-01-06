using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day19;

public class Day19Solver : DaySolver
{
	private Rules _rules;
	private IList<string> _messages;

	public Day19Solver(string inputFilePath) : base(inputFilePath)
	{
		IEnumerator<string> it = InputLines.GetEnumerator();
		List<string> rulesLines = new List<string>();
		while (it.MoveNext() && it.Current != "")
		{
			rulesLines.Add(it.Current);
		}
		_rules = new Rules(rulesLines.Count);
		foreach (string line in rulesLines)
		{
			(string numString, string ruleString) = line.SplitIntoTwo(':', StringSplitOptions.TrimEntries);
			int number = int.Parse(numString);
			IRule rule = _rules.GetRuleFromString(ruleString);
			_rules[number] = rule;
		}
		while (it.Current == "") it.MoveNext();
		_messages = new List<string>();
		while (it.Current != "")
		{
			_messages.Add(it.Current);
			if (!it.MoveNext()) break;
		}
	}

	public override string SolvePart1()
	{
		Regex r = new Regex("^" + _rules[0].GetRegexString() + "$");
		int result = _messages.Count(message => r.IsMatch(message));
		return result.ToString();
	}

	public override string SolvePart2()
	{
		int max = _messages.Max(str => str.Length);
		_rules.ResetCaches();
		_rules[8] = new OneOrMoreRule(_rules[42]);
		_rules[11] = new WrappedInTwoRulesRule(_rules[42], _rules[31], max);
		Regex r = new Regex("^" + _rules[0].GetRegexString() + "$");
		int result = _messages.Count(message => r.IsMatch(message));
		return result.ToString();
	}
}
