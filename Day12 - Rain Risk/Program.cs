using System;
using System.IO;

namespace Day12
{
    class Program
    {
        static void ExecuteCommand(IShip ship, string command)
        {
            char action = command[0];
            int value = int.Parse(command.Substring(1));
            switch (action)
            {
                case 'N':
                    ship.GoNorth(value);
                    break;
                case 'S':
                    ship.GoSouth(value);
                    break;
                case 'E':
                    ship.GoEast(value);
                    break;
                case 'W':
                    ship.GoWest(value);
                    break;
                case 'L':
                    ship.TurnLeft(value);
                    break;
                case 'R':
                    ship.TurnRight(value);
                    break;
                case 'F':
                    ship.GoForward(value);
                    break;
            }
        }

        static int GetManhattanDistanceOfShip(IShip ship, string[] instructions)
        {
            foreach (string instruction in instructions)
            {
                ExecuteCommand(ship, instruction);
            }
            return ship.Position.ManhattanDistance();
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
                    string[] lines = reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);

                    int distance = 0;
                    distance = GetManhattanDistanceOfShip(new Ship(), lines);
                    System.Console.WriteLine($"Manhattan distance: {distance}");
                    distance = GetManhattanDistanceOfShip(new WaypointShip(), lines);
                    System.Console.WriteLine($"Waypoint manhattan distance: {distance}");
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
