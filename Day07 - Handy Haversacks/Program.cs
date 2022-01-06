using System;

using AdventOfCode.Year2020.Day07;

const string DEFAULT_INPUT_FILEPATH = "input.txt";

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
var solver = new Day07Solver(filepath);

Console.Write("Part 1: ");
string part1 = solver.SolvePart1();
System.Console.WriteLine(part1);

Console.Write("Part 2: ");
string part2 = solver.SolvePart2();
System.Console.WriteLine(part2);
