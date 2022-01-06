namespace AdventOfCode.Year2020.Day12;

public interface IShip
{
	public Vector Position { get; }

	public void GoNorth(int value);
	public void GoSouth(int value);
	public void GoEast(int value);
	public void GoWest(int value);
	public void TurnLeft(int angle);
	public void TurnRight(int angle);
	public void TurnBack();
	public void GoForward(int value);
}
