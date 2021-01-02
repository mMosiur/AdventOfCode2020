using System.Linq;

namespace Day17
{
    public partial class CubeSpace4D
    {
        private Cube4D[,,,] _cubes;

        public int xLength => _cubes.GetLength(0);
        public int yLength => _cubes.GetLength(1);
        public int zLength => _cubes.GetLength(2);
        public int wLength => _cubes.GetLength(3);

        public CubeSpace4D(int xSize, int ySize, int zSize, int wSize)
        {
            _cubes = new Cube4D[xSize, ySize, zSize, wSize];
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    for (int z = 0; z < zSize; z++)
                    {
                        for (int w = 0; w < wSize; w++)
                        {
                            _cubes[x, y, z, w] = new Cube4D(this, x, y, z, w);
                        }
                    }
                }
            }
        }

        public Cube4D this[int x, int y, int z, int w]
        {
            get => _cubes[x, y, z, w];
            set => _cubes[x, y, z, w] = value;
        }

        public int ActiveCount
        {
            get
            {
                int count = 0;
                foreach (var el in _cubes)
                {
                    if (el.Active) count++;
                }
                return count;
            }
        }

        public void SimulateCycle()
        {
            Cube4D[,,,] copy = new Cube4D[xLength, yLength, zLength, wLength];
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    for (int z = 0; z < zLength; z++)
                    {
                        for (int w = 0; w < wLength; w++)
                        {
                            Cube4D cube = _cubes[x, y, z, w];
                            bool newState = cube.Active;
                            int count = cube.Neighbors.Count(neighbor => neighbor.Active);
                            if (cube.Active)
                            {
                                newState = (count == 2 || count == 3);
                            }
                            else
                            {
                                newState = (count == 3);
                            }
                            copy[x, y, z, w] = new Cube4D(this, x, y, z, w, newState);
                        }
                    }
                }
            }
            _cubes = copy;
        }

        public CubeSpace4D(int size) : this(size, size, size, size) { }
        
    }
}