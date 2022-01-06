using System.Linq;

namespace AdventOfCode.Year2020.Day17;

public partial class CubeSpace3D
{
	private Cube3D[,,] _cubes;

	public int xLength => _cubes.GetLength(0);
	public int yLength => _cubes.GetLength(1);
	public int zLength => _cubes.GetLength(2);

	public CubeSpace3D(int xSize, int ySize, int zSize)
	{
		_cubes = new Cube3D[xSize, ySize, zSize];
		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				for (int z = 0; z < zSize; z++)
				{
					_cubes[x, y, z] = new Cube3D(this, x, y, z);
				}
			}
		}
	}

	public Cube3D this[int x, int y, int z]
	{
		get => _cubes[x, y, z];
		set => _cubes[x, y, z] = value;
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
		Cube3D[,,] copy = new Cube3D[xLength, yLength, zLength];
		for (int x = 0; x < xLength; x++)
		{
			for (int y = 0; y < yLength; y++)
			{
				for (int z = 0; z < zLength; z++)
				{
					Cube3D cube = _cubes[x, y, z];
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
					copy[x, y, z] = new Cube3D(this, x, y, z, newState);
				}
			}
		}
		_cubes = copy;
	}

	public CubeSpace3D(int size) : this(size, size, size) { }

}
