using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode.Year2020.Day14;

public class MemoryV1 : IMemory
{
	private Dictionary<long, long> memory = new Dictionary<long, long>();
	private BitArray maskTrue = new BitArray(36, false);
	private BitArray maskFalse = new BitArray(36, true);

	public void SetMask(string str)
	{
		maskTrue.SetAll(false);
		maskFalse.SetAll(true);
		for (int i = 0; i < str.Length; i++)
		{
			switch (str[i])
			{
				case 'X':
					break;
				case '1':
					maskTrue.Set(i, true);
					break;
				case '0':
					maskFalse.Set(i, false);
					break;
				default:
					throw new ArgumentException();
			}
		}
	}

	public void Write(long address, long value)
	{
		memory[address] = MaskValue(value);
	}

	public long GetMemorySum()
	{
		long sum = 0;
		foreach (long value in memory.Values)
			sum += value;
		return sum;
	}

	private long MaskValue(long value)
	{
		value |= BitArrayToLong(maskTrue);
		value &= BitArrayToLong(maskFalse);
		return value;
	}

	private long BitArrayToLong(BitArray array)
	{
		long number = 0;
		for (int i = 0; i < array.Length; i++)
		{
			number <<= 1;
			if (array[i])
				number++;
		}
		return number;
	}
}
