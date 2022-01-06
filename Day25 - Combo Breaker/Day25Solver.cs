using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day25;

public class Day25Solver : DaySolver
{
	private readonly int _cardPublicKey;
	private readonly int _doorPublicKey;

	public Day25Solver(string inputFilePath) : base(inputFilePath)
	{
		_cardPublicKey = int.Parse(InputLines.First());
		_doorPublicKey = int.Parse(InputLines.Skip(1).First());
	}

	private int BruteForceLoopSizeFromPublicKey(int subjectNumber, int publicKey)
	{
		int value = 1;
		for (int loopSize = 0; loopSize < 20201227; loopSize++)
		{
			if (value == publicKey) return loopSize;
			value = (int)((long)value * subjectNumber) % 20201227;
		}
		throw new Exception();
	}

	private int Transform(int subjectNumber, int loopSize)
	{
		int value = 1;
		for (int i = 0; i < loopSize; i++)
			value = (int)((long)value * subjectNumber % 20201227);
		return value;
	}

	public override string SolvePart1()
	{
		int cardLoopSize = BruteForceLoopSizeFromPublicKey(7, _cardPublicKey);
		int doorLoopSize = BruteForceLoopSizeFromPublicKey(7, _doorPublicKey);
		int encryptionKey = Transform(_cardPublicKey, doorLoopSize);
		return encryptionKey.ToString();
	}

	public override string SolvePart2() => string.Empty;
}
