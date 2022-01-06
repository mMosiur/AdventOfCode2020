using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Day16;

public class Ticket : List<int>
{
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

	public override string ToString()
	{
		return string.Join(',', this);
	}
}
