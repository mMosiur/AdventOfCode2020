using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Day22
{
    public class DeckPairComparer : IEqualityComparer<(Deck, Deck)>
    {
        public bool Equals((Deck, Deck) x, (Deck, Deck) y)
        {
            bool result = Enumerable.SequenceEqual(x.Item1, y.Item1) && Enumerable.SequenceEqual(x.Item2, y.Item2);
            return result;
        }

        public int GetHashCode([DisallowNull] (Deck, Deck) obj)
        {
            return obj.GetHashCode();
        }
    }

    public class Game2 : IGame
    {
        private Deck deck1;
        private Deck deck2;

        private List<(Deck, Deck)> previous;

        public Game2(Deck deck1, Deck deck2)
        {
            previous = new List<(Deck, Deck)>();
            this.deck1 = deck1;
            this.deck2 = deck2;
            Winner = null;
        }

        public Deck Winner { get; private set; }

        bool IsRepeat()
        {
            bool isRepeat = previous.Contains((deck1, deck2), new DeckPairComparer());
            return isRepeat;
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
            if (IsRepeat())
            {
                Winner = deck1;
                return false;
            }
            previous.Add((deck1.GetCopy(), deck2.GetCopy()));
            Card card1 = deck1.GetTopCard();
            Card card2 = deck2.GetTopCard();
            if (deck1.Count >= card1.Number && deck2.Count >= card2.Number)
            {
                Deck deck1copy = deck1.GetCopy(card1.Number);
                Deck deck2copy = deck2.GetCopy(card2.Number);
                IGame recursiveGame = new Game2(deck1copy, deck2copy);
                recursiveGame.Play();
                if (recursiveGame.Winner == deck1copy)
                {
                    deck1.PutOnBottom(card1, card2);
                }
                else if (recursiveGame.Winner == deck2copy)
                {
                    deck2.PutOnBottom(card2, card1);
                }
                else throw new Exception();
            }
            else
            {
                if (card1 > card2)
                {
                    deck1.PutOnBottom(card1, card2);
                }
                else if (card1 < card2)
                {
                    deck2.PutOnBottom(card2, card1);
                }
                else throw new Exception();
            }
            return true;
        }

        public Deck Play()
        {
            bool nextRound = true;
            while (nextRound)
            {
                nextRound = PlayRound();
            }
            return Winner;
        }
    }
}