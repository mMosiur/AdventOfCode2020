using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day07;

public class Day07Solver : DaySolver
{
	private Luggage _luggage;

	public Day07Solver(string inputFilePath) : base(inputFilePath)
	{
		_luggage = new Luggage();
		foreach (string line in InputLines)
		{
			string[] strPart = line.Split(' ', 5);
			string color = strPart[0] + " " + strPart[1];
			Bag bag = _luggage.GetOrCreateNew(color);
			strPart = strPart[4].Split(' ', 5);
			while (strPart.Length >= 4)
			{
				int count = int.Parse(strPart[0]);
				color = strPart[1] + " " + strPart[2];
				bag.Add(_luggage.GetOrCreateNew(color), count);
				if (strPart.Length == 4) break;
				strPart = strPart[4].Split(' ', 5);
			}
		}
	}

	public override string SolvePart1()
	{
		const string COLOR = "shiny gold";
		int result = _luggage.CountBagsThatContain(COLOR);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		const string COLOR = "shiny gold";
		int result = _luggage.CountBagsIn(COLOR);
		return result.ToString();
	}
}
