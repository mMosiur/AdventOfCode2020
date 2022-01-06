using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020.Day04;

public class Passport
{
	private static readonly string[] RequiredFields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

	private Dictionary<string, string> _fields = new Dictionary<string, string>();

	public void AddPassportField(string field)
	{
		string[] parts = field.Split(':');
		if (parts.Length != 2)
		{
			throw new FormatException($"Invalid passport field: \"{field}\"");
		}
		_fields.Add(parts[0], parts[1]);
	}

	public int FieldCount => _fields.Count;

	public bool IsValid
	{
		get
		{
			// return RequiredFields.All(field => _fields.ContainsKey(field));
			return _fields.ContainsKey("byr")
				&& _fields.ContainsKey("iyr")
				&& _fields.ContainsKey("eyr")
				&& _fields.ContainsKey("hgt")
				&& _fields.ContainsKey("hcl")
				&& _fields.ContainsKey("ecl")
				&& _fields.ContainsKey("pid");
		}
	}

	public bool IsStrictlyValid
	{
		get
		{
			// byr (Birth Year) - four digits; at least 1920 and at most 2002.
			string value = _fields.GetValueOrDefault("byr") ?? "";
			if (value.Length != 4) return false;
			if (!int.TryParse(value, out int num)) return false;
			if (num < 1920 || num > 2002) return false;
			// iyr (Issue Year) - four digits; at least 2010 and at most 2020.
			value = _fields.GetValueOrDefault("iyr") ?? "";
			if (value.Length != 4) return false;
			if (!int.TryParse(value, out num)) return false;
			if (num < 2010 || num > 2020) return false;
			// eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
			value = _fields.GetValueOrDefault("eyr") ?? "";
			if (value.Length != 4) return false;
			if (!int.TryParse(value, out num)) return false;
			if (num < 2020 || num > 2030) return false;
			// hgt (Height) - a number followed by either cm or in:
			//  If cm, the number must be at least 150 and at most 193.
			//  If in, the number must be at least 59 and at most 76.
			value = _fields.GetValueOrDefault("hgt") ?? "";
			if (value == "") return false;
			string value1 = value.Substring(0, value.Length - 2);
			string value2 = value.Substring(value.Length - 2);
			if (!int.TryParse(value1, out num)) return false;
			if (value2 == "cm")
			{
				if (num < 150 || num > 193) return false;
			}
			else if (value2 == "in")
			{
				if (num < 59 || num > 76) return false;
			}
			else return false;
			// hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
			value = _fields.GetValueOrDefault("hcl") ?? "";
			if (value.Length != 7) return false;
			if (value[0] != '#') return false;
			if (!int.TryParse(value.Substring(1), System.Globalization.NumberStyles.HexNumber, null, out num)) return false;
			// ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
			value = _fields.GetValueOrDefault("ecl") ?? "";
			if (value != "amb" && value != "blu" && value != "brn" && value != "gry" && value != "grn" && value != "hzl" && value != "oth")
				return false;
			// pid (Passport ID) - a nine-digit number, including leading zeroes.
			value = _fields.GetValueOrDefault("pid") ?? "";
			if (value.Length != 9) return false;
			if (!int.TryParse(value, out num)) return false;
			return true;
		}
	}
}
