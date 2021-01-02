using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    class Program
    {
        static (Rules, Ticket, List<Ticket>) GetInput(string inputFilePath)
        {
            FileStream filestream = null;
            try
            {
                filestream = new(inputFilePath, FileMode.Open);
                using (StreamReader reader = new(filestream))
                {
                    string[] blocks = reader.ReadToEnd().Split("\n\n");
                    // Rules
                    Rules rules = new Rules();
                    foreach (string line in blocks[0].Split('\n', StringSplitOptions.RemoveEmptyEntries))
                    {
                        string[] entries = line.Split(new string[] { ":", " or " }, StringSplitOptions.TrimEntries);
                        if (entries.Length != 3) throw new FormatException();
                        string name = entries[0];
                        Range range1 = new Range(entries[1]);
                        Range range2 = new Range(entries[2]);
                        rules.Add(name, range1, range2);
                    }
                    // My ticket
                    string[] lines = blocks[1].Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    if (lines.Length != 2) throw new FormatException();
                    Ticket myTicket = new Ticket(lines[1].Split(',', StringSplitOptions.TrimEntries).Select(str => int.Parse(str)));
                    // Other tickets
                    List<Ticket> tickets = new List<Ticket>();
                    foreach (string line in blocks[2].Split('\n', StringSplitOptions.RemoveEmptyEntries).Skip(1))
                    {
                        tickets.Add(new Ticket(line.Split(',', StringSplitOptions.TrimEntries).Select(str => int.Parse(str))));
                    }
                    return (rules, myTicket, tickets);
                }
            }
            finally
            {
                if (filestream != null)
                {
                    filestream.Dispose();
                }
            }
        }

        static void Main(string[] args)
        {
            var (rules, myTicket, tickets) = GetInput("input.txt");
            // Part 1
            int error = tickets.Sum(ticket => ticket.GetTicketError(rules));
            Console.WriteLine($"Error: {error}");
            // Part 2
            tickets = tickets.Where(ticket => ticket.IsValid(rules)).ToList();
            Dictionary<string, int> fieldIndexes = new();
            HashSet<string>[] possibleFields = new HashSet<string>[myTicket.Count];
            for (int i = 0; i < myTicket.Count; i++)
                possibleFields[i] = new HashSet<string>(rules.Keys);
            while (possibleFields.Any(set=>set.Count > 0)) // While there is at least one index with more options
            {
                for (int i = 0; i < myTicket.Count; i++)
                {
                    foreach (Ticket ticket in tickets)
                    {
                        possibleFields[i].IntersectWith(rules.MathingRules(ticket[i]).Select(rule=>rule.Name));
                    }
                }
                for (int i = 0; i < myTicket.Count; i++)
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
                product *= myTicket[index];
            Console.WriteLine($"Sum of departures: {product}");
        }
    }
}
