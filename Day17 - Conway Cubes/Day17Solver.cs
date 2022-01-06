using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day17;

public class Day17Solver : DaySolver
{
	public Day17Solver(string inputFilePath) : base(inputFilePath)
	{
	}

	private CubeSpace3D GetCubeSpace3D()
	{
		string[] lines = InputLines.ToArray();
		if (!lines.All(line => line.Length == lines.Length))
			throw new FormatException();
		CubeSpace3D cube = new CubeSpace3D(lines.Length + 6 * 2);
		int z = cube.zLength / 2;
		for (int x = 0; x < lines.Length; x++)
		{
			for (int y = 0; y < lines[x].Length; y++)
			{
				if (lines[x][y] != '#') continue;
				cube[x + 6, y + 6, z].Active = true;
			}
		}
		return cube;
	}

	private CubeSpace4D GetCubeSpace4D()
	{
		string[] lines = InputLines.ToArray();
		if (!lines.All(line => line.Length == lines.Length))
			throw new FormatException(nameof(lines));
		CubeSpace4D cube = new CubeSpace4D(lines.Length + 6 * 2);
		int w = cube.wLength / 2;
		int z = cube.zLength / 2;
		for (int x = 0; x < lines.Length; x++)
		{
			for (int y = 0; y < lines[x].Length; y++)
			{
				if (lines[x][y] != '#') continue;
				cube[x + 6, y + 6, z, w].Active = true;
			}
		}
		return cube;
	}

	public override string SolvePart1()
	{
		CubeSpace3D cubeSpace3D = GetCubeSpace3D();
		for (int i = 0; i < 6; i++)
		{
			cubeSpace3D.SimulateCycle();
		}
		int result = cubeSpace3D.ActiveCount;
		return result.ToString();
	}

	public override string SolvePart2()
	{
		CubeSpace4D cubeSpace4D = GetCubeSpace4D();
		for (int i = 0; i < 6; i++)
		{
			cubeSpace4D.SimulateCycle();
		}
		int result = cubeSpace4D.ActiveCount;
		return result.ToString();
	}
}
