using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Day07;

public class Luggage
{
	private Dictionary<string, Bag> bags = new();

	public Bag GetOrCreateNew(string color)
	{
		if (!bags.TryGetValue(color, out Bag? bag))
		{
			bag = new Bag(color);
			bags.Add(color, bag);
		}
		return bag;
	}

	public int CountBagsThatContain(string color)
	{
		return bags.Values.Count(bag => bag.Contains(color));
	}

	public int CountBagsIn(string color)
	{
		Bag bag = GetOrCreateNew(color);
		return bag.Count;
	}
}
