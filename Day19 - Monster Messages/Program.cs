using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ExtensionMethods;

namespace Day19
{
    class Program
    {
        const string InputFilePath = "input.txt";
        static void Main(string[] args)
        {
            string rulesString, messagesString;

            FileStream filestream = null;
            try
            {
                filestream = new(InputFilePath, FileMode.Open);
                using (StreamReader reader = new(filestream))
                {
                    (rulesString, messagesString) = reader.ReadToEnd().SplitIntoTwo("\n\n");
                }
            }
            finally
            {
                if (filestream != null)
                {
                    filestream.Dispose();
                }
            }
            string[] rulesLines = rulesString.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            Rules rules = new Rules(rulesLines.Length);
            foreach (string line in rulesLines)
            {
                var (numString, ruleString) = line.SplitIntoTwo(':', StringSplitOptions.TrimEntries);
                int number = int.Parse(numString);
                IRule rule = rules.GetRuleFromString(ruleString);
                rules[number] = rule;
            }
            string[] messages = messagesString.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int max = messages.Max(str => str.Length);
            Regex r = new Regex("^" + rules[0].GetRegexString() + "$");
            int count = messages.Count(message => r.IsMatch(message));
            System.Console.WriteLine($"Count 1: {count}");
            rules.ResetCaches();
            rules[8] = new OneOrMoreRule(rules[42]);
            rules[11] = new WrappedInTwoRulesRule(rules[42], rules[31], max);
            r = new Regex("^" + rules[0].GetRegexString() + "$");
            count = messages.Count(message => r.IsMatch(message));
            System.Console.WriteLine($"Count 2: {count}");
        }
    }
}
