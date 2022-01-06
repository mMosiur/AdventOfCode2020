using System;

namespace AdventOfCode.Year2020.Day22;

public class Game1 : IGame
{
	private Deck deck1;
	private Deck deck2;

	public Deck? Winner { get; private set; }

	public Game1(Deck deck1, Deck deck2)
	{
		this.deck1 = deck1;
		this.deck2 = deck2;
		Winner = null;
	}

	public bool PlayRound()
	{
		if (Winner != null) return false;
		if (deck1.Count > 0 && deck2.Count == 0)
		{
			Winner = deck1;
			return false;
		}
		if (deck1.Count == 0 && deck2.Count > 0)
		{
			Winner = deck2;
			return false;
		}
		Card card1 = deck1.GetTopCard();
		Card card2 = deck2.GetTopCard();
		if (card1 > card2)
		{
			deck1.PutOnBottom(card1, card2);
		}
		else if (card1 < card2)
		{
			deck2.PutOnBottom(card2, card1);
		}
		else throw new Exception();
		return true;
	}

	public Deck Play()
	{
		bool nextRound = true;
		while (nextRound)
		{
			nextRound = PlayRound();
		}
		return Winner ?? throw new InvalidOperationException();
	}
}
