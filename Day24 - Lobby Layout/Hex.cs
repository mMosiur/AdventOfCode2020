using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Day24;

public enum Color
{
	White,
	Black
}

public class Hex
{
	private HexGrid grid;
	private CubeCoordinates coordinates;

	public Color Color { get; set; }


	public IEnumerable<Hex> Neighbors
	{
		get
		{
			yield return E;
			yield return SE;
			yield return SW;
			yield return W;
			yield return NW;
			yield return NE;
		}
	}
	public Hex E => grid[coordinates.East];
	public Hex NE => grid[coordinates.NorthEast];
	public Hex SE => grid[coordinates.SouthEast];
	public Hex W => grid[coordinates.West];
	public Hex NW => grid[coordinates.NorthWest];
	public Hex SW => grid[coordinates.SouthWest];

	public IEnumerable<Hex> BlackNeighbors => Neighbors.Where(hex => hex.Color == Color.Black);
	public IEnumerable<Hex> WhiteNeighbors => Neighbors.Where(hex => hex.Color == Color.White);

	public Hex(HexGrid grid, CubeCoordinates coordinates, Color color = Color.White)
	{
		this.grid = grid;
		this.coordinates = coordinates;
		Color = color;
	}

	public void Flip()
	{
		Color = Color == Color.White ? Color.Black : Color.White;
	}
}
