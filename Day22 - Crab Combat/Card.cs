using System;

namespace AdventOfCode.Year2020.Day22;

public class Card : IComparable
{
	public int Number { get; }

	public Card(int number)
	{
		Number = number;
	}

	public static implicit operator Card(int number) => new Card(number);

	public static bool operator ==(Card c1, Card c2) => c1.Number == c2.Number;
	public static bool operator !=(Card c1, Card c2) => c1.Number != c2.Number;
	public static bool operator <(Card c1, Card c2) => c1.Number < c2.Number;
	public static bool operator >(Card c1, Card c2) => c1.Number > c2.Number;
	public static bool operator <=(Card c1, Card c2) => c1.Number <= c2.Number;
	public static bool operator >=(Card c1, Card c2) => c1.Number >= c2.Number;

	public override string ToString()
	{
		return Number.ToString();
	}

	public int CompareTo(object? obj)
	{
		if (obj is null) throw new ArgumentNullException(nameof(obj));
		Card other = (Card)obj;
		return Number.CompareTo(other.Number);
	}

	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType())
			return false;
		Card other = (Card)obj;
		return Number.Equals(other.Number);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}
