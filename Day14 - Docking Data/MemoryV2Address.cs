using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Day14;

public class MemoryV2Address
{
	public MemoryV2Address(bool?[] array)
	{
		this.array = array;
	}

	bool?[] array;

	public IEnumerable<long> GetAddresses()
	{
		int floatingCount = array.Count(b => b == null);
		for (long num = 0; num < (1 << floatingCount); num++)
		{
			bool[] address = new bool[array.Length];
			for (int i = 0; i < array.Length; i++)
				address[i] = array[i] ?? false;
			int pos = 0;
			for (int i = 0; i < floatingCount; i++)
			{
				while (array[pos] != null) pos++;
				address[pos] = GetBit(i, num);
				pos++;
			}
			yield return BoolArrayToLong(address);
		}
	}

	private long BoolArrayToLong(bool[] array)
	{
		long value = 0;
		foreach (bool bit in array)
		{
			value <<= 1;
			if (bit) value++;
		}
		return value;
	}

	private bool GetBit(int index, long value)
	{
		long mask = 1 << index;
		value &= mask;
		return value != 0;
	}
}
