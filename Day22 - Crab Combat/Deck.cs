using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Year2020.Day22;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class Deck : IEnumerable<Card>, IEnumerable
{
	LinkedList<Card> list;

	public Deck(IEnumerable<Card> collection)
	{
		list = new LinkedList<Card>(collection);
	}

	public int Count => list.Count;

	public Card GetTopCard()
	{
		Card top = list.First?.Value ?? throw new InvalidOperationException();
		list.RemoveFirst();
		return top;
	}

	public void PutOnBottom(Card card)
	{
		list.AddLast(card);
	}

	public void PutOnBottom(Card card1, Card card2)
	{
		list.AddLast(card1);
		list.AddLast(card2);
	}

	public int Score
	{
		get
		{
			int score = 0;
			int i = list.Count;
			foreach (Card card in list)
			{
				score += card.Number * i;
				i--;
			}
			return score;
		}
	}

	public Deck GetCopy()
	{
		return new Deck(list);
	}

	public Deck GetCopy(int count)
	{
		return new Deck(list.Take(count));
	}

	public IEnumerator<Card> GetEnumerator()
	{
		return list.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	private string GetDebuggerDisplay()
	{
		return $"Count: {Count}";
	}
}
