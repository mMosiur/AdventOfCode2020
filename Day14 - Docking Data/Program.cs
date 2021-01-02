using System;
using System.IO;

namespace Day14
{
    class Program
    {
        const string InputFilePath = "input.txt";

        static long GetSumOfMemory(IMemory memory, string[] instructions)
        {
            foreach (string instruction in instructions)
            {
                string[] command = instruction.Split(" = ");
                if (command[0] == "mask")
                {
                    memory.SetMask(command[1]);
                }
                else
                {
                    command[0] = command[0].Substring(4, command[0].Length - 5);
                    int address = int.Parse(command[0]);
                    long value = long.Parse(command[1]);
                    memory.Write(address, value);
                }
            }
            return memory.GetMemorySum();
        }

        static void Main(string[] args)
        {
            FileStream filestream = null;
            try
            {
                filestream = new(InputFilePath, FileMode.Open);
                using (StreamReader reader = new(filestream))
                {
                    string[] lines = reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);

                    long sum1 = GetSumOfMemory(new MemoryV1(), lines);
                    System.Console.WriteLine($"Memory v1 sum: {sum1}");
                    long sum2 = GetSumOfMemory(new MemoryV2(), lines);
                    System.Console.WriteLine($"Memory v2 sum: {sum2}");
                }
            }
            finally
            {
                if (filestream != null)
                {
                    filestream.Dispose();
                }
            }
        }
    }
}
