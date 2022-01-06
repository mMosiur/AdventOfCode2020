namespace AdventOfCode.Year2020.Day24;

public struct CubeCoordinates
{
	public int X { get; set; }
	public int Y { get; set; }
	public int Z { get; set; }

	public CubeCoordinates(int x, int y, int z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	public CubeCoordinates East => new CubeCoordinates(X + 1, Y - 1, Z);
	public CubeCoordinates NorthEast => new CubeCoordinates(X + 1, Y, Z - 1);
	public CubeCoordinates SouthEast => new CubeCoordinates(X, Y - 1, Z + 1);
	public CubeCoordinates West => new CubeCoordinates(X - 1, Y + 1, Z);
	public CubeCoordinates NorthWest => new CubeCoordinates(X, Y + 1, Z - 1);
	public CubeCoordinates SouthWest => new CubeCoordinates(X - 1, Y, Z + 1);
}
