using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day18;

public class Day18Solver : DaySolver
{
	public Day18Solver(string inputFilePath) : base(inputFilePath)
	{
	}

	private Queue<char> CreateEquationQueue(string equation, bool precedence)
	{
		Queue<char> queue = new Queue<char>();
		Stack<char> stack = new Stack<char>();
		using (StringReader reader = new StringReader(equation))
		{
			while (reader.Peek() >= 0)
			{
				char symbol = (char)reader.Read();
				if (symbol == ' ') continue;
				if (char.IsDigit(symbol))
				{
					queue.Enqueue(symbol);
				}
				else if (symbol == '(')
				{
					stack.Push(symbol);
				}
				else if (symbol == ')')
				{
					while (stack.Count > 0 && stack.Peek() != '(')
					{
						queue.Enqueue(stack.Pop());
					}
					if (stack.Peek() == '(') stack.Pop();
				}
				else // e is an operator
				{
					while (stack.Count > 0 && stack.Peek() != '(')
					{
						if (precedence && symbol == '+' && stack.Peek() == '*')
							break;
						queue.Enqueue(stack.Pop());
					}
					stack.Push(symbol);
				}
			}
			while (stack.Count > 0)
				queue.Enqueue(stack.Pop());
		}
		return queue;
	}

	private long ResolveEquation(Queue<char> equation)
	{
		Stack<long> stack = new Stack<long>();
		foreach (char symbol in equation)
		{
			if (char.IsDigit(symbol))
			{
				stack.Push((long)symbol - (long)'0');
			}
			else // operator
			{
				switch (symbol)
				{
					case '+':
						stack.Push(stack.Pop() + stack.Pop());
						break;
					case '*':
						stack.Push(stack.Pop() * stack.Pop());
						break;
					default:
						throw new ArithmeticException("Unknown operator");
				}
			}
		}
		if (stack.Count != 1) throw new ArgumentException("Unresolvable equation");
		return stack.Pop();
	}

	public override string SolvePart1()
	{
		long result = InputLines
			.Select(line => CreateEquationQueue(line, false))
			.Select(equation => ResolveEquation(equation))
			.Sum();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		long result = InputLines
			.Select(line => CreateEquationQueue(line, true))
			.Select(equation => ResolveEquation(equation))
			.Sum();
		return result.ToString();
	}
}
