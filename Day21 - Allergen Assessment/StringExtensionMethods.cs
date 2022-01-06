using System;

namespace AdventOfCode.Year2020.Day21;

public static class ExtensionMethods
{
	public static (string, string) SplitIntoTwo(this string str, string separator, StringSplitOptions options = StringSplitOptions.None)
	{
		string[] parts = str.Split(separator, options);
		if (parts.Length != 2)
			throw new Exception();
		return (parts[0], parts[1]);
	}

	public static (string, string) SplitIntoTwo(this string str, char separator, StringSplitOptions options = StringSplitOptions.None)
	{
		string[] parts = str.Split(separator, options);
		if (parts.Length != 2)
			throw new Exception();
		return (parts[0], parts[1]);
	}
}
