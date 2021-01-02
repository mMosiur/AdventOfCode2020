using System.Collections.Generic;

namespace Day17
{
    public partial class CubeSpace4D
    {
        public class Cube4D
        {
            private readonly CubeSpace4D _space;
            public int X { get; }
            public int Y { get; }
            public int Z { get; }
            public int W { get; }
            public bool Active { get; set; }

            public Cube4D(CubeSpace4D space, int x, int y, int z, int w, bool state = false)
            {
                _space = space;
                X = x;
                Y = y;
                Z = z;
                W = w;
                Active = state;
            }

            public IEnumerable<Cube4D> Neighbors
            {
                get
                {
                    for (int xOffset = -1; xOffset < 2; xOffset++)
                    {
                        int x = X + xOffset;
                        if (x < 0 || x >= _space.xLength) continue;
                        for (int yOffset = -1; yOffset < 2; yOffset++)
                        {
                            int y = Y + yOffset;
                            if (y < 0 || y >= _space.yLength) continue;
                            for (int zOffset = -1; zOffset < 2; zOffset++)
                            {
                                int z = Z + zOffset;
                                if (z < 0 || z >= _space.zLength) continue;
                                for (int wOffset = -1; wOffset < 2; wOffset++)
                                {
                                    int w = W + wOffset;
                                    if (w < 0 || w >= _space.wLength) continue;
                                    if (xOffset == 0 && yOffset == 0 && zOffset == 0 && wOffset == 0) continue;
                                    yield return _space[x, y, z, w];
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}