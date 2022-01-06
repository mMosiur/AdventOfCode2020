using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Day16;

public class Rules : IEnumerable<Rule>
{
	private Dictionary<string, Rule> rules = new();

	public void Add(string name, Range range1, Range range2)
	{
		rules.Add(name, new Rule(name, range1, range2));
	}

	public Dictionary<string, Rule>.KeyCollection Keys => rules.Keys;

	public IEnumerable<Rule> MathingRules(int number)
	{
		return rules.Values.Where(rules => rules.DoesMatch(number));
	}

	public IEnumerator<Rule> GetEnumerator()
	{
		return rules.Values.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
