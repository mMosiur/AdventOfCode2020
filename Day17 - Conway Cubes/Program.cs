using System;
using System.IO;
using System.Linq;

namespace Day17
{
    class Program
    {

        static string[] ReadFile(string path)
        {
            FileStream filestream = null;
            try
            {
                filestream = new FileStream(path, FileMode.Open);
                using (StreamReader reader = new StreamReader(filestream))
                {
                    return reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
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

        static CubeSpace3D GetCubeSpace3DFromInput(string[] input)
        {
            CubeSpace3D cube = null;
            if (!input.All(line => line.Length == input.Length))
                throw new FileLoadException();
            cube = new CubeSpace3D(input.Length + 6 * 2);
            int z = cube.zLength / 2;
            for (int x = 0; x < input.Length; x++)
            {
                for (int y = 0; y < input[x].Length; y++)
                {
                    if(input[x][y] == '#')
                        cube[x+6, y+6, z].Active = true;
                }
            }
            return cube;
        }

        static CubeSpace4D GetCubeSpace4DFromInput(string[] input)
        {
            CubeSpace4D cube = null;
            if (!input.All(line => line.Length == input.Length))
                throw new FileLoadException();
            cube = new CubeSpace4D(input.Length + 6 * 2);
            int w = cube.wLength / 2;
            int z = cube.zLength / 2;
            for (int x = 0; x < input.Length; x++)
            {
                for (int y = 0; y < input[x].Length; y++)
                {
                    if(input[x][y] == '#')
                        cube[x+6, y+6, z, w].Active = true;
                }
            }
            return cube;
        }

        static void Main(string[] args)
        {
            string[] lines = ReadFile("input.txt");
            CubeSpace3D cubespace3D = GetCubeSpace3DFromInput(lines);
            for (int i = 0; i < 6; i++)
            {
                cubespace3D.SimulateCycle();
            }
            System.Console.WriteLine($"Active cubes in 3D: {cubespace3D.ActiveCount}");
            CubeSpace4D cubespace4D = GetCubeSpace4DFromInput(lines);
            for (int i = 0; i < 6; i++)
            {
                cubespace4D.SimulateCycle();
            }
            System.Console.WriteLine($"Active cubes in 4D: {cubespace4D.ActiveCount}");
        }
    }
}
