using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    public class Tile : IEnumerable<char>
    {
        protected char[,] _tile;

        public int ID { get; }

        public char this[int row, int col]
        {
            get
            {
                return _tile[row, col];
            }
        }

        public int GetLength(int dimension) => _tile.GetLength(dimension);

        public Tile(int id, char[,] tile)
        {
            this.ID = id;
            _tile = tile;
        }

        public IEnumerable<IEnumerable<char>> Edges
        {
            get
            {
                yield return TopEdge;
                yield return RightEdge;
                yield return BottomEdge;
                yield return LeftEdge;
            }
        }

        public IEnumerable<int> EdgeHashes
        {
            get
            {
                yield return TopEdgeHash;
                yield return RightEdgeHash;
                yield return BottomEdgeHash;
                yield return LeftEdgeHash;
            }
        }

        public IEnumerable<char> TopEdge
        {
            get
            {
                int r = 0;
                for (int c = 0; c < _tile.GetLength(1); c++)
                {
                    yield return _tile[r, c];
                }
            }
        }
        public int TopEdgeHash => GetEdgeHash(TopEdge);
        public IEnumerable<char> BottomEdge
        {
            get
            {
                int r = _tile.GetLength(0) - 1;
                for (int c = 0; c < _tile.GetLength(1); c++)
                {
                    yield return _tile[r, c];
                }
            }
        }
        public int BottomEdgeHash => GetEdgeHash(BottomEdge);

        public IEnumerable<char> LeftEdge
        {
            get
            {
                int c = 0;
                for (int r = 0; r < _tile.GetLength(0); r++)
                {
                    yield return _tile[r, c];
                }
            }
        }
        public int LeftEdgeHash => GetEdgeHash(LeftEdge);
        public IEnumerable<char> RightEdge
        {
            get
            {
                int c = _tile.GetLength(1) - 1;
                for (int r = 0; r < _tile.GetLength(0); r++)
                {
                    yield return _tile[r, c];
                }
            }
        }
        public int RightEdgeHash => GetEdgeHash(RightEdge);

        public static int GetEdgeHash(IEnumerable<char> edge)
        {
            bool[] edgeArray = edge.Select(c=>c=='#').ToArray();
            bool[] reverseEdgeArray = edgeArray.Reverse().ToArray();
            bool[] bigger = BiggerArray(edgeArray, reverseEdgeArray);
            return BoolArrayToInt(bigger);
        }

        public static int BoolArrayToInt(bool[] arr)
        {
            if (arr.Length > 31) throw new ArgumentException("Array too big");
            int result = 0;
            foreach (bool el in arr)
            {
                result <<= 1;
                if(el) result++;
            }
            return result;
        }

        public static bool[] BiggerArray(bool[] arr1, bool[] arr2)
        {
            foreach (var (First, Second) in arr1.Zip(arr2))
            {
                if (First && !Second) return arr1;
                if (!First && Second) return arr2;
            }
            return arr1;
        }

        public void FlipHorizontally()
        {
            char[,] copy = new char[_tile.GetLength(0), _tile.GetLength(1)];
            for (int r = 0; r < _tile.GetLength(0); r++)
            {
                for (int c = 0; c < _tile.GetLength(1); c++)
                {
                    copy[r, c] = _tile[r, _tile.GetLength(1) - 1 - c];
                }
            }
            _tile = copy;
        }

        public void FlipVertically()
        {
            char[,] copy = new char[_tile.GetLength(0), _tile.GetLength(1)];
            for (int r = 0; r < _tile.GetLength(0); r++)
            {
                for (int c = 0; c < _tile.GetLength(1); c++)
                {
                    copy[r, c] = _tile[_tile.GetLength(0) - 1 - r, c];
                }
            }
            _tile = copy;
        }

        public void RotateClockwise()
        {
            char[,] copy = new char[_tile.GetLength(1), _tile.GetLength(0)];
            for (int r = 0; r < _tile.GetLength(1); r++)
            {
                for (int c = 0; c < _tile.GetLength(0); c++)
                {
                    int or = _tile.GetLength(0) - 1 - c;
                    int oc = r;
                    copy[r, c] = _tile[or, oc];
                }
            }
            _tile = copy;
        }

        public void Flip()
        {
            char[,] copy = new char[_tile.GetLength(0), _tile.GetLength(1)];
            for (int r = 0; r < _tile.GetLength(0); r++)
            {
                for (int c = 0; c < _tile.GetLength(1); c++)
                {
                    copy[r, c] = _tile[_tile.GetLength(0) - 1 - r, _tile.GetLength(1) - 1 - c];
                }
            }
            _tile = copy;
        }

        public void RotateCounterclockwise()
        {
            char[,] copy = new char[_tile.GetLength(1), _tile.GetLength(0)];
            for (int r = 0; r < _tile.GetLength(1); r++)
            {
                for (int c = 0; c < _tile.GetLength(0); c++)
                {
                    int or = c;
                    int oc = _tile.GetLength(1) - 1 - r;
                    copy[r, c] = _tile[or, oc];
                }
            }
            _tile = copy;
        }

        public IEnumerable<Tile> Rotations
        {
            get
            {
                yield return this;
                RotateClockwise();
                yield return this;
                RotateClockwise();
                yield return this;
                RotateClockwise();
                yield return this;
                FlipHorizontally();
                yield return this;
                RotateClockwise();
                yield return this;
                RotateClockwise();
                yield return this;
                RotateClockwise();
                yield return this;
                FlipHorizontally();
            }
        }

        public override string ToString()
        {
            string str = $"Tile {ID}:\n";
            for (int r = 0; r < _tile.GetLength(0); r++)
            {
                for (int c = 0; c < _tile.GetLength(1); c++)
                {
                    str += _tile[r, c];
                }
                str += '\n';
            }
            return str;
        }

        public IEnumerator<char> GetEnumerator()
        {
            return _tile.Cast<char>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}