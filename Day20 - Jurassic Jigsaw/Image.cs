using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    public class Image : Tile
    {

        public Image(char[,] image) : base(0, image)
        {
        }

        private (int, int)[] SeaMonsterOffsets = new[] { (0, 18), (1, 0), (1, 5), (1, 6), (1, 11), (1, 12), (1, 17), (1, 18), (1, 19), (2, 1), (2, 4), (2, 7), (2, 10), (2, 13), (2, 16) };

        public int SeaMonsterSize => SeaMonsterOffsets.Length;

        public int CountSeaMonsters()
        {
            int count = 0;
            for (int r = 0; r < GetLength(0) - 2; r++)
            {
                for (int c = 0; c < GetLength(1) - 19; c++)
                {
                    if (SeaMonsterOffsets.Select(p => this[r + p.Item1, c + p.Item2]).Any(c => c == '.'))
                        continue;
                    count++;
                }
            }
            return count;
        }
    }
}