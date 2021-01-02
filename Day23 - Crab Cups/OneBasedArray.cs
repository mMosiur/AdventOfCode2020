using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day23
{
    class OneBasedArray<T> : IEnumerable<T>
    {
        private T[] array;

        public int Current { get; set; }

        public OneBasedArray(int size)
        {
            array = new T[size];
        }

        public T this[int index] { get => array[index - 1]; set => array[index - 1] = value; }

        public int Length => array.Length;
        public long LongLength => array.LongLength;

        public IEnumerator<T> GetEnumerator()
        {
            return array.Cast<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}