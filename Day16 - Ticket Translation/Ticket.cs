using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day16
{
    public class Ticket : List<int>
    {
        public Ticket()
        {
        }

        public Ticket(IEnumerable<int> collection) : base(collection)
        {
        }

        public int GetTicketError(Rules rules)
        {
            return this.Where(el => !rules.MathingRules(el).Any()).Sum();
        }

        public bool IsValid(Rules rules)
        {
            return this.All(el => rules.MathingRules(el).Any());
        }

        public IEnumerable<int> GetMatchingIndexes(Rule rule)
        {
            return Enumerable.Range(0, Count).Where(i => rule.DoesMatch(this[i]));
        }

        public override string ToString()
        {
            return string.Join(',', this);
        }
    }
}