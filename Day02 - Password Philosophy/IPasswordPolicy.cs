namespace AdventOfCode.Year2020.Day02;

public interface IPasswordPolicy
{
	public bool DoesMatch(string password);
}
