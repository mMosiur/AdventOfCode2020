using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day13;

public class Day13Solver : DaySolver
{
	private readonly int _timestamp;
	private readonly int[] _buses;

	public Day13Solver(string inputFilePath) : base(inputFilePath)
	{
		_timestamp = int.Parse(InputLines.First());
		_buses = InputLines
			.Skip(1)
			.Single()
			.Split(',')
			.Select(str => str == "x" ? 0 : int.Parse(str))
			.ToArray();
	}

	private int GetBestBusMultipliedByWaitTime(int[] buses, int timestamp)
	{
		Tuple<int, int> best = new Tuple<int, int>(0, int.MaxValue);
		foreach (int bus in buses.Where(el => el > 0))
		{
			int wait = (bus - (timestamp % bus)) % bus;
			if (wait < best.Item2)
			{
				best = new Tuple<int, int>(bus, wait);
			}
		}
		return best.Item1 * best.Item2;
	}

	private long GetEarliestTimestampOfMinuteByMinuteDepartures(int[] buses)
	{
		long t = 1;
		long step = 1;
		for (int i = 0; i < buses.Length; i++)
		{
			if (buses[i] == 0) continue;
			while ((t + i) % buses[i] != 0) t += step;
			step = Math.LCM(step, buses[i]);
		}
		return t;
	}
	public override string SolvePart1()
	{
		int result = GetBestBusMultipliedByWaitTime(_buses, _timestamp);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		long result = GetEarliestTimestampOfMinuteByMinuteDepartures(_buses);
		return result.ToString();
	}
}
