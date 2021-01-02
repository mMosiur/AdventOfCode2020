using System;
using System.IO;

namespace Day25
{
    class Program
    {
        static (int, int) GetInput(string path)
        {
            FileStream filestream = null;
            try
            {
                filestream = new FileStream(path, FileMode.Open);
                using (StreamReader reader = new StreamReader(filestream))
                {
                    string[] lines = reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    if (lines.Length != 2) throw new Exception();
                    return (int.Parse(lines[0]), int.Parse(lines[1]));
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
        static int Transform(int subjectNumber, int loopSize)
        {
            int value = 1;
            for (int i = 0; i < loopSize; i++)
                value = (int)((long)value * subjectNumber % 20201227);
            return value;
        }

        static int BruteForceLoopSizeFromPublicKey(int subjectNumber, int publicKey)
        {
            int value = 1;
            for (int loopSize = 0; loopSize < 20201227; loopSize++)
            {
                if (value == publicKey) return loopSize;
                value = (int)((long)value * subjectNumber) % 20201227;
            }
            throw new Exception();
        }

        static void Main(string[] args)
        {
            var (cardPublicKey, doorPublicKey) = GetInput("input.txt");
            int cardLoopSize = BruteForceLoopSizeFromPublicKey(7, cardPublicKey);
            int doorLoopSize = BruteForceLoopSizeFromPublicKey(7, doorPublicKey);
            System.Console.WriteLine($"Card secret loop size: {cardLoopSize}");
            System.Console.WriteLine($"Door secret loop size: {doorLoopSize}");
            int encryptionKey = Transform(cardPublicKey, doorLoopSize);
            System.Console.WriteLine($"Encryption key: {encryptionKey}");
        }
    }
}
