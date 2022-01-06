using System;
using System.Collections.Generic;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day22;

public class Day22Solver : DaySolver
{
	private Deck _deck1;
	private Deck _deck2;

	public Day22Solver(string inputFilePath) : base(inputFilePath)
	{
		var it = InputLines.GetEnumerator();
		it.MoveNext();
		while ("".Equals(it.Current)) it.MoveNext();
		it.MoveNext();
		List<Card> deck1Cards = new();
		while (!"".Equals(it.Current))
		{
			int number = int.Parse(it.Current);
			deck1Cards.Add(new Card(number));
			it.MoveNext();
		}
		while ("".Equals(it.Current)) it.MoveNext();
		it.MoveNext();
		List<Card> deck2Cards = new();
		while (!"".Equals(it.Current))
		{
			int number = int.Parse(it.Current);
			deck2Cards.Add(new Card(number));
			if(!it.MoveNext()) break;
		}
		_deck1 = new Deck(deck1Cards);
		_deck2 = new Deck(deck2Cards);
	}

	public override string SolvePart1()
	{
		IGame game = new Game1(_deck1.GetCopy(), _deck2.GetCopy());
		game.Play();
		if (game.Winner is null)
		{
			throw new InvalidOperationException("No winner after the game.");
		}
		int score = game.Winner.Score;
		return score.ToString();
	}

	public override string SolvePart2()
	{
		IGame game = new Game2(_deck1.GetCopy(), _deck2.GetCopy());
		game.Play();
		if (game.Winner is null)
		{
			throw new InvalidOperationException("No winner after the game.");
		}
		int score = game.Winner.Score;
		return score.ToString();
	}
}
