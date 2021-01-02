using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    class MemoryV2Address
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
                if(bit) value++;
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

    public class MemoryV2 : IMemory
    {
        private Dictionary<long, long> memory = new Dictionary<long, long>();
        private BitArray bitMask = new BitArray(36, false);
        private BitArray floatingMask = new BitArray(36, false);

        public void SetMask(string str)
        {
            bitMask.SetAll(false);
            floatingMask.SetAll(false);
            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case 'X':
                        floatingMask.Set(i, true);
                        break;
                    case '1':
                        bitMask.Set(i, true);
                        break;
                    case '0':
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public long GetMemorySum()
        {
            long sum = 0;
            foreach (long value in memory.Values)
                sum += value;
            return sum;
        }

        public void Write(long address, long value)
        {
            foreach (long adr in MaskAdress(address).GetAddresses())
            {
                memory[adr] = value;
            }
        }

        MemoryV2Address MaskAdress(long address)
        {
            address |= BitArrayToLong(bitMask);
            bool?[] array = LongToBooleanArray(address);
            for (int i = 0; i < floatingMask.Length; i++)
            {
                if(floatingMask[i]) array[i] = null;
            }
            return new MemoryV2Address(array);
        }

        private bool?[] LongToBooleanArray(long value)
        {
            return Convert.ToString(value, 2).PadLeft(36, '0').Select<char, bool?>(s => s.Equals('1')).ToArray();
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
}