using System;

using AdventOfCode.Year2020.Day25;

const string DEFAULT_INPUT_FILEPATH = "input.txt";

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
var solver = new Day25Solver(filepath);

Console.Write("Part 1: ");
string part1 = solver.SolvePart1();
Console.WriteLine(part1);
