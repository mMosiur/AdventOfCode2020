using System.Linq;

namespace AdventOfCode.Year2020.Day19;

public interface IRule
{
	public string GetRegexString();
	public void ResetCache();
}

public class ProxyRule : IRule
{
	private Rules rules;
	private int index;

	public ProxyRule(Rules rules, int index)
	{
		this.rules = rules;
		this.index = index;
	}

	public string GetRegexString()
	{
		return rules[index].GetRegexString();
	}

	public void ResetCache()
	{
	}
}

public class LiteralRule : IRule
{
	private string literal;

	public LiteralRule(string literal)
	{
		this.literal = literal;
	}

	public string GetRegexString()
	{
		return $"({literal})";
	}

	public void ResetCache()
	{
	}
}

public class AndRule : IRule
{
	private IRule rule1;
	private IRule rule2;

	private string? regexString;

	public AndRule(IRule rule1, IRule rule2)
	{
		this.rule1 = rule1;
		this.rule2 = rule2;
		regexString = null;
	}

	public string GetRegexString()
	{
		return regexString ??= $"({rule1.GetRegexString()})({rule2.GetRegexString()})";
	}

	public void ResetCache()
	{
		regexString = null;
	}
}

public class OrRule : IRule
{
	private IRule rule1;
	private IRule rule2;

	private string? regexString;

	public OrRule(IRule rule1, IRule rule2)
	{
		this.rule1 = rule1;
		this.rule2 = rule2;
		regexString = null;
	}

	public string GetRegexString()
	{
		return regexString ??= $"({rule1.GetRegexString()})|({rule2.GetRegexString()})";
	}

	public void ResetCache()
	{
		regexString = null;
	}
}

public class OneOrMoreRule : IRule
{
	private IRule rule;

	public OneOrMoreRule(IRule rule)
	{
		this.rule = rule;
	}

	public string GetRegexString()
	{
		return $"({rule.GetRegexString()})+";
	}

	public void ResetCache()
	{
	}
}

public class WrappedInTwoRulesRule : IRule
{
	private IRule ruleL;
	private IRule ruleR;
	private int maxLength;

	public WrappedInTwoRulesRule(IRule ruleL, IRule ruleR, int maxLength)
	{
		this.ruleL = ruleL;
		this.ruleR = ruleR;
		this.maxLength = maxLength;
	}

	public string GetRegexString()
	{
		string left = "(" + ruleL.GetRegexString() + ")";
		string right = "(" + ruleR.GetRegexString() + ")";
		string result = "(" + string.Join('|', Enumerable.Range(1, maxLength / 2).Select(i => $"{left}{{{i}}}{right}{{{i}}}")) + ")";
		return result;
	}

	public void ResetCache()
	{
	}
}
