namespace Day14
{
    public interface IMemory
    {
        public void SetMask(string str);
        public void Write(long address, long value);
        public long GetMemorySum();
    }
}