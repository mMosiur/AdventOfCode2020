using System.Collections.Generic;

namespace Day17
{
    public partial class CubeSpace3D
    {
        public class Cube3D
        {
            private readonly CubeSpace3D _space;
            public int X { get; }
            public int Y { get; }
            public int Z { get; }
            public bool Active { get; set; }

            public Cube3D(CubeSpace3D space, int x, int y, int z, bool state = false)
            {
                _space = space;
                X = x;
                Y = y;
                Z = z;
                Active = state;
            }

            public IEnumerable<Cube3D> Neighbors
            {
                get
                {
                    for (int xOffset = -1; xOffset < 2; xOffset++)
                    {
                        int x = X + xOffset;
                        if(x < 0 || x >= _space.xLength) continue;
                        for (int yOffset = -1; yOffset < 2; yOffset++)
                        {
                            int y = Y + yOffset;
                            if(y < 0 || y >= _space.yLength) continue;
                            for (int zOffset = -1; zOffset < 2; zOffset++)
                            {
                                int z = Z + zOffset;
                                if (z < 0 || z >= _space.zLength) continue;
                                if (xOffset == 0 && yOffset == 0 && zOffset == 0) continue;
                                yield return _space[x, y, z];
                            }
                        }
                    }
                }
            }
        }
    }
}