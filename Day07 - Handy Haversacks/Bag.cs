using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Day07;

public class Bag
{
	public string Color { get; }
	private IDictionary<Bag, int> Content { get; }

	public Bag(string color)
	{
		Color = color;
		Content = new Dictionary<Bag, int>();
	}

	public bool Contains(string color) => Content.Keys.Any(bag => bag.Color == color || bag.Contains(color));

	public int Count => Content.Sum(kvp => (kvp.Key.Count + 1) * kvp.Value);

	public void Add(Bag bag, int count)
	{
		Content.Add(bag, count);
	}
}
