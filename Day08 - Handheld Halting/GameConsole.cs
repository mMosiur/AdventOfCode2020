using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Day08;

public partial class GameConsole
{
	private string[] _program;

	public IReadOnlyList<string> Program => _program;
	public int Accumulator { get; private set; }

	public GameConsole(IEnumerable<string> program)
	{
		_program = program.ToArray();
	}

	public bool TryRepairCorruptedProgram()
	{
		for (int i = 0; i < _program.Length; i++)
		{
			string[] command = Program[i].Split(' ');
			switch (command[0])
			{
				case "jmp":
					_program[i] = "nop " + command[1];
					try
					{
						Run();
						return true;
					}
					catch (InfiniteLoopException) { }
					_program[i] = "jmp " + command[1];
					break;
				case "nop":
					_program[i] = "jmp " + command[1];
					try
					{
						Run();
						return true;
					}
					catch (InfiniteLoopException) { }
					_program[i] = "nop " + command[1];
					break;
				default:
					break;
			}
		}
		return false;
	}

	public void Run()
	{
		Accumulator = 0;
		int pc = 0;
		bool[] commandsRun = new bool[_program.Length];
		while (pc < _program.Length)
		{
			if (commandsRun[pc])
				throw new InfiniteLoopException();
			else commandsRun[pc] = true;
			string[] command = Program[pc].Split(' ');
			if (command.Length != 2)
				throw new CorruptedProgramException();
			int arg;
			if (!int.TryParse(command[1], out arg))
				throw new CorruptedProgramException();
			switch (command[0])
			{
				case "acc":
					Accumulator += arg;
					pc++;
					break;
				case "jmp":
					pc += arg;
					break;
				case "nop":
					pc++;
					break;
				default:
					throw new CorruptedProgramException();
			}
		}
	}
}
