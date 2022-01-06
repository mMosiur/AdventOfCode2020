namespace AdventOfCode.Year2020.Day12;

public struct Vector
{
	public Vector(int x, int y)
	{
		X = x;
		Y = y;
	}

	public int X { get; set; }

	public int Y { get; set; }

	public static Vector operator *(Vector vec1, Vector vec2)
	{
		return new Vector(vec1.X * vec2.X, vec1.Y * vec2.Y);
	}

	public static Vector operator *(Vector vec, int scalar)
	{
		return new Vector(vec.X * scalar, vec.Y * scalar);
	}

	public static Vector operator +(Vector vec1, Vector vec2)
	{
		return new Vector(vec1.X + vec2.X, vec1.Y + vec2.Y);
	}

	public void RotateLeft()
	{
		int temp = X;
		X = -Y;
		Y = temp;
	}

	public void RotateRight()
	{
		int temp = X;
		X = Y;
		Y = -temp;
	}

	public void Flip()
	{
		X = -X;
		Y = -Y;
	}

	public int ManhattanDistance()
	{
		return System.Math.Abs(X) + System.Math.Abs(Y);
	}

	public override string ToString()
	{
		return $"[{X},{Y}]";
	}

	public static readonly Vector North = new Vector(0, 1);
	public static readonly Vector South = new Vector(0, -1);
	public static readonly Vector East = new Vector(1, 0);
	public static readonly Vector West = new Vector(-1, 0);
}
