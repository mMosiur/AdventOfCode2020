using System;
using System.Linq;

namespace AdventOfCode.Year2020.Day20;

public class TileGrid
{
	private Tile?[,] grid;

	public TileGrid(int rowCount, int colCount)
	{
		grid = new Tile?[rowCount, colCount];
	}

	public Tile? this[int row, int column]
	{
		get
		{
			if (row < 0 || row >= grid.GetLength(0)) return null;
			if (column < 0 || column >= grid.GetLength(1)) return null;
			return grid[row, column];
		}
		set => grid[row, column] = value;
	}

	public Tile? this[Index row, Index column]
	{
		get
		{
			int iRow = row.IsFromEnd ? grid.GetLength(0) - row.Value : row.Value;
			int iCol = column.IsFromEnd ? grid.GetLength(0) - column.Value : column.Value;
			return grid[iRow, iCol];
		}
		set
		{
			int iRow = row.IsFromEnd ? grid.GetLength(0) - row.Value : row.Value;
			int iCol = column.IsFromEnd ? grid.GetLength(0) - column.Value : column.Value;
			this[iRow, iCol] = value;
		}
	}

	public bool InsertAt(int row, int column, Tile tile)
	{
		if (this[row, column] != null) throw new Exception();
		this[row, column] = tile;
		foreach (Tile rotation in tile.Rotations)
		{
			Tile? top = this[row - 1, column];
			Tile? bottom = this[row + 1, column];
			Tile? left = this[row, column - 1];
			Tile? right = this[row, column + 1];
			if (top != null && !top.BottomEdge.SequenceEqual(tile.TopEdge)) continue;
			if (bottom != null && !bottom.TopEdge.SequenceEqual(tile.BottomEdge)) continue;
			if (left != null && !left.RightEdge.SequenceEqual(tile.LeftEdge)) continue;
			if (right != null && !right.LeftEdge.SequenceEqual(tile.RightEdge)) continue;
			return true;
		}
		this[row, column] = null;
		return false;
	}

	public int GetLength(int dimension) => grid.GetLength(dimension);

	public Image Merge()
	{
		int tileLength = grid[0, 0]?.GetLength(0) ?? throw new InvalidOperationException();
		int side = tileLength - 2; // We know each tile is a square of equal side length
		char[,] image = new char[grid.GetLength(0) * side, grid.GetLength(1) * side];
		for (int tileRow = 0; tileRow < grid.GetLength(0); tileRow++)
		{
			for (int tileCol = 0; tileCol < grid.GetLength(1); tileCol++)
			{
				Tile tile = grid[tileRow, tileCol] ?? throw new InvalidOperationException();
				for (int r = 0; r < tile.GetLength(0) - 2; r++)
				{
					for (int c = 0; c < tile.GetLength(1) - 2; c++)
					{
						image[tileRow * side + r, tileCol * side + c] = tile[r + 1, c + 1];
					}
				}
			}
		}
		return new Image(image);
	}
}
