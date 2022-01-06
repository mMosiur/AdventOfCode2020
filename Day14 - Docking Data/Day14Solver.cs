using System.Collections.Generic;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day14;

public class Day14Solver : DaySolver
{
	private readonly IEnumerable<string> _instructions;

	public Day14Solver(string inputFilePath) : base(inputFilePath)
	{
		_instructions = InputLines;
	}

	private long GetSumOfMemory(IMemory memory, IEnumerable<string> instructions)
	{
		foreach (string instruction in instructions)
		{
			string[] command = instruction.Split(" = ");
			if (command[0] == "mask")
			{
				memory.SetMask(command[1]);
			}
			else
			{
				command[0] = command[0].Substring(4, command[0].Length - 5);
				int address = int.Parse(command[0]);
				long value = long.Parse(command[1]);
				memory.Write(address, value);
			}
		}
		return memory.GetMemorySum();
	}

	public override string SolvePart1()
	{
		IMemory memory = new MemoryV1();
		long result = GetSumOfMemory(memory, _instructions);
		return result.ToString();
	}

	public override string SolvePart2()
	{
		IMemory memory = new MemoryV2();
		long result = GetSumOfMemory(memory, _instructions);
		return result.ToString();
	}
}
