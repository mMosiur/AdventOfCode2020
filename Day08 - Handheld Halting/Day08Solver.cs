using System.IO;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day08;

public class Day08Solver : DaySolver
{
	private GameConsole _gameConsole;

	public Day08Solver(string inputFilePath) : base(inputFilePath)
	{
		_gameConsole = new GameConsole(InputLines);
	}

	public override string SolvePart1()
	{
		try
		{
			_gameConsole.Run();
		}
		catch (GameConsole.InfiniteLoopException) { }
		int result = _gameConsole.Accumulator;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		bool repaired = _gameConsole.TryRepairCorruptedProgram();
		if (!repaired)
		{
			throw new InvalidDataException("Could not repair program");
		}
		// Not needed to run again because TryRepairCorruptedProgram in itself runs after every iteration
		int result = _gameConsole.Accumulator;
		return result.ToString();
	}
}
