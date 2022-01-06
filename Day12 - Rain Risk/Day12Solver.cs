using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day12;

public class Day12Solver : DaySolver
{
	private readonly string[] _instructions;

	public Day12Solver(string inputFilePath) : base(inputFilePath)
	{
		_instructions = InputLines.ToArray();
	}


	private void ExecuteCommand(IShip ship, string command)
	{
		char action = command[0];
		int value = int.Parse(command.Substring(1));
		switch (action)
		{
			case 'N':
				ship.GoNorth(value);
				break;
			case 'S':
				ship.GoSouth(value);
				break;
			case 'E':
				ship.GoEast(value);
				break;
			case 'W':
				ship.GoWest(value);
				break;
			case 'L':
				ship.TurnLeft(value);
				break;
			case 'R':
				ship.TurnRight(value);
				break;
			case 'F':
				ship.GoForward(value);
				break;
		}
	}

	private int GetManhattanDistanceOfShip(IShip ship, string[] instructions)
	{
		foreach (string instruction in instructions)
		{
			ExecuteCommand(ship, instruction);
		}
		return ship.Position.ManhattanDistance();
	}

	public override string SolvePart1()
	{
		IShip ship = new Ship();
		int result = GetManhattanDistanceOfShip(ship, _instructions);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		IShip ship = new WaypointShip();
		int result = GetManhattanDistanceOfShip(ship, _instructions);
		return result.ToString();
	}
}
