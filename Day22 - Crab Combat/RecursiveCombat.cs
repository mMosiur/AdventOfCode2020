using System;

namespace Day22
{
    public class RecursiveCombat
    {
        private Deck deck1;
        private Deck deck2;

        public RecursiveCombat(Deck deck1, Deck deck2)
        {
            this.deck1 = deck1;
            this.deck2 = deck2;
        }

        public Deck GetWinner()
        {
            Deck d1 = deck1.GetCopy();
            Deck d2 = deck2.GetCopy();
            Card c1 = d1.GetTopCard();
            Card c2 = d2.GetTopCard();
            throw new NotImplementedException();
        }
    }
}