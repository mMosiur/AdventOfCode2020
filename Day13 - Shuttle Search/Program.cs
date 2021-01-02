using System;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {

        static int GetBestBusMultipliedByWaitTime(int[] buses, int timestamp)
        {
            Tuple<int, int> best = new Tuple<int, int>(0, int.MaxValue);
            foreach (int bus in buses.Where(el => el > 0))
            {
                int wait = (bus - (timestamp % bus)) % bus;
                if (wait < best.Item2)
                {
                    best = new Tuple<int, int>(bus, wait);
                }
            }
            return best.Item1 * best.Item2;
        }

        static long GetEarliestTimestampOfMinuteByMinuteDepartures(int[] buses)
        {
            long t = 1;
            long step = 1;
            for (int i = 0; i < buses.Length; i++)
            {
                if(buses[i] == 0) continue;
                while ((t+i) % buses[i] != 0) t += step;
                step = Math.LCM(step, buses[i]);
            }
            return t;
        }

        const string InputFilePath = "input.txt";

        static void Main(string[] args)
        {
            FileStream filestream = null;
            try
            {
                filestream = new(InputFilePath, FileMode.Open);
                using (StreamReader reader = new(filestream))
                {
                    int timestamp = int.Parse(reader.ReadLine());
                    int[] buses = reader.ReadLine().Split(',').Select(str => str == "x" ? 0 : int.Parse(str)).ToArray();
                    int part1 = GetBestBusMultipliedByWaitTime(buses, timestamp);
                    Console.WriteLine($"{part1}");
                    long part2 = GetEarliestTimestampOfMinuteByMinuteDepartures(buses);
                    Console.WriteLine($"{part2}");
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
