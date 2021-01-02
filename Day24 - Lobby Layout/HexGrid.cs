using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day24
{
    public class HexGrid : IEnumerable<Hex>
    {
        private Dictionary<CubeCoordinates, Hex> hexes = new Dictionary<CubeCoordinates, Hex>();

        public void FlipHexAt(string path)
        {
            using (StringReader reader = new StringReader(path))
            {
                char prev = char.MinValue;
                int i;
                CubeCoordinates coords = new CubeCoordinates();
                while ((i = reader.Read()) >= 0)
                {
                    char c = (char)i;
                    switch (c)
                    {
                        case 'e':
                            if (prev == 's')
                                coords = coords.SouthEast;
                            else if (prev == 'n')
                                coords = coords.NorthEast;
                            else
                                coords = coords.East;
                            prev = 'e';
                            break;
                        case 'w':
                            if (prev == 's')
                                coords = coords.SouthWest;
                            else if (prev == 'n')
                                coords = coords.NorthWest;
                            else
                                coords = coords.West;
                            prev = 'w';
                            break;
                        case 's':
                            prev = 's';
                            break;
                        case 'n':
                            prev = 'n';
                            break;
                        default:
                            throw new FormatException();
                    }
                }
                this[coords].Flip();
            }

        }

        public IEnumerator<Hex> GetEnumerator()
        {
            return hexes.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<Hex> WhiteHexes => this.Where(hex => hex.Color == Color.White);
        public IEnumerable<Hex> BlackHexes => this.Where(hex => hex.Color == Color.Black);

        public Hex this[CubeCoordinates coordinates]
        {
            get
            {
                Hex hex;
                if (!hexes.TryGetValue(coordinates, out hex))
                {
                    hex = new Hex(this, coordinates);
                    hexes.Add(coordinates, hex);
                }
                return hex;
            }
            set
            {
                hexes[coordinates] = value;
            }
        }

        public void NextDay()
        {
            HashSet<Hex> hexesToFlip = new HashSet<Hex>();
            HashSet<Hex> whitesToCheck = new HashSet<Hex>();
            foreach (Hex hex in BlackHexes.ToList())
            {
                whitesToCheck.UnionWith(hex.WhiteNeighbors);
                int blackCount = hex.BlackNeighbors.Count();
                if (blackCount == 0 || blackCount > 2)
                    hexesToFlip.Add(hex);
            }
            foreach (Hex hex in whitesToCheck)
            {
                int blackCount = hex.BlackNeighbors.Count();
                if (blackCount == 2)
                    hexesToFlip.Add(hex);
            }
            foreach (Hex hex in hexesToFlip)
            {
                hex.Flip();
            }
        }
    }
}