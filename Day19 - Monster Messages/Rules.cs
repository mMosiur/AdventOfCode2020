using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day19
{
    public class Rules : IEnumerable<IRule>
    {
        IRule[] rules;

        public Rules(int count)
        {
            rules = new IRule[count];
        }

        public IRule this[int index]
        {
            get => rules[index];
            set => rules[index] = value;
        }

        public IEnumerator<IRule> GetEnumerator()
        {
            return rules.Cast<IRule>().GetEnumerator();
        }

        public IRule GetRuleFromString(string str)
        {
            str = str.Trim();
            string[] parts = str.Split('|', 2, StringSplitOptions.TrimEntries);
            if (parts.Length == 2)
            {
                IRule rule1 = GetRuleFromString(parts[0]);
                IRule rule2 = GetRuleFromString(parts[1]);
                return new OrRule(rule1, rule2);
            }
            else
            {
                parts = str.Split(' ', 2, StringSplitOptions.TrimEntries);
                if (parts.Length == 2)
                {
                    IRule rule1 = GetRuleFromString(parts[0]);
                    IRule rule2 = GetRuleFromString(parts[1]);
                    return new AndRule(rule1, rule2);
                }
                else if (str.StartsWith('"') && str.EndsWith('"'))
                {
                    return new LiteralRule(str.Substring(1, str.Length - 2));
                }
                else
                {
                    int number = int.Parse(str);
                    return new ProxyRule(this, number);
                }
            }
        }

        public void ResetCaches()
        {
            foreach (IRule rule in rules)
            {
                rule?.ResetCache();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}