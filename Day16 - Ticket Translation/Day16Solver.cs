using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day16;

public class Day16Solver : DaySolver
{
	private Rules _rules;
	private Ticket _myTicket;
	private List<Ticket> _tickets;

	public Day16Solver(string inputFilePath) : base(inputFilePath)
	{
		IEnumerator<string> it = InputLines.GetEnumerator();

		_rules = new Rules();
		if (!it.MoveNext()) throw new FormatException();
		while (it.Current != "")
		{
			string[] entries = it.Current.Split(new string[] { ":", " or " }, StringSplitOptions.RemoveEmptyEntries);
			if (entries.Length != 3) throw new FormatException();
			string name = entries[0];
			Range range1 = new Range(entries[1]);
			Range range2 = new Range(entries[2]);
			_rules.Add(name, range1, range2);
			if (!it.MoveNext()) throw new FormatException();
		}

		while (it.Current == "") if (!it.MoveNext()) throw new FormatException();

		if (!it.MoveNext()) throw new FormatException();
		if (it.Current == "") throw new FormatException();
		_myTicket = new Ticket(it.Current.Split(',', StringSplitOptions.TrimEntries).Select(int.Parse));
		if (!it.MoveNext()) throw new FormatException();
		if (it.Current != "") throw new FormatException();

		while (it.Current == "") if (!it.MoveNext()) throw new FormatException();

		_tickets = new List<Ticket>();
		while (it.MoveNext() && it.Current != "")
		{
			_tickets.Add(new Ticket(it.Current.Split(',', StringSplitOptions.TrimEntries).Select(int.Parse)));
		}
	}

	public override string SolvePart1()
	{
		int error = _tickets.Sum(ticket => ticket.GetTicketError(_rules));
		return error.ToString();
	}

	public override string SolvePart2()
	{
		List<Ticket> tickets = _tickets.Where(ticket => ticket.IsValid(_rules)).ToList();
		Dictionary<string, int> fieldIndexes = new();
		HashSet<string>[] possibleFields = new HashSet<string>[_myTicket.Count];
		for (int i = 0; i < _myTicket.Count; i++)
			possibleFields[i] = new HashSet<string>(_rules.Keys);
		while (possibleFields.Any(set => set.Count > 0)) // While there is at least one index with more options
		{
			for (int i = 0; i < _myTicket.Count; i++)
			{
				foreach (Ticket ticket in tickets)
				{
					possibleFields[i].IntersectWith(_rules.MathingRules(ticket[i]).Select(rule => rule.Name));
				}
			}
			for (int i = 0; i < _myTicket.Count; i++)
			{
				if (possibleFields[i].Count == 1) // The only possibility for this index
				{
					string field = possibleFields[i].Single();
					fieldIndexes[field] = i; // Save found index of given field
					foreach (var set in possibleFields)
						set.ExceptWith(Enumerable.Repeat(field, 1)); // Delete this field from every index (including this). This is going to leave this index with 0 possible fields.
				}
			}
		}
		long product = 1;
		foreach (int index in fieldIndexes.Where(pair => pair.Key.StartsWith("departure")).Select(pair => pair.Value))
		{
			product *= _myTicket[index];
		}
		return product.ToString();
	}
}
