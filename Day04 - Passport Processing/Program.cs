using System;
using System.Collections.Generic;
using System.IO;

namespace Day04
{
    class Program
    {
        static IEnumerable<Dictionary<string, string>> GetData()
        {
            using (StreamReader stream = new("input.txt"))
            {
                string[] passports = stream.ReadToEnd().Split("\n\n");
                foreach (string passport in passports)
                {
                    Dictionary<string, string> dict = new();
                    foreach (string line in passport.Split("\n"))
                    {
                        if (string.IsNullOrEmpty(line)) continue;
                        foreach (string entry in line.Split(' '))
                        {
                            string[] splitEntry = entry.Split(':', 2);
                            dict[splitEntry[0]] = splitEntry[1];
                        }
                    }
                    yield return dict;
                }
            }
        }

        static bool IsPassportValid(Dictionary<string, string> passport)
        {
            return passport.ContainsKey("byr")
                && passport.ContainsKey("iyr")
                && passport.ContainsKey("eyr")
                && passport.ContainsKey("hgt")
                && passport.ContainsKey("hcl")
                && passport.ContainsKey("ecl")
                && passport.ContainsKey("pid");
        }

        static bool IsPassportStrictlyValid(Dictionary<string, string> passport)
        {
            int num;
            string value;
            // byr (Birth Year) - four digits; at least 1920 and at most 2002.
            value = passport.GetValueOrDefault("byr") ?? "";
            if(value.Length != 4) return false;
            if(!int.TryParse(value, out num)) return false;
            if(num < 1920 || num > 2002) return false;
            // iyr (Issue Year) - four digits; at least 2010 and at most 2020.
            value = passport.GetValueOrDefault("iyr") ?? "";
            if(value.Length != 4) return false;
            if(!int.TryParse(value, out num)) return false;
            if(num < 2010 || num > 2020) return false;
            // eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
            value = passport.GetValueOrDefault("eyr") ?? "";
            if(value.Length != 4) return false;
            if(!int.TryParse(value, out num)) return false;
            if(num < 2020 || num > 2030) return false;
            // hgt (Height) - a number followed by either cm or in:
            //  If cm, the number must be at least 150 and at most 193.
            //  If in, the number must be at least 59 and at most 76.
            value = passport.GetValueOrDefault("hgt") ?? "";
            if(value == "") return false;
            string value1 = value.Substring(0, value.Length - 2);
            string value2 = value.Substring(value.Length - 2);
            if(value2 == "cm")
            {
                if(!int.TryParse(value1, out num)) return false;
                if(num < 150 || num > 193) return false;
            }
            else if(value2 == "in")
            {
                if(!int.TryParse(value1, out num)) return false;
                if(num < 59 || num > 76) return false;
            }
            else return false;
            // hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            value = passport.GetValueOrDefault("hcl") ?? "";
            if(value.Length != 7) return false;
            if(value[0] != '#') return false;
            if(!int.TryParse(value.Substring(1), System.Globalization.NumberStyles.HexNumber, null, out num)) return false;
            // ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            value = passport.GetValueOrDefault("ecl") ?? "";
            if(value != "amb" && value != "blu" && value != "brn" && value != "gry" && value != "grn" && value != "hzl" && value != "oth")
                return false;
            // pid (Passport ID) - a nine-digit number, including leading zeroes.
            value = passport.GetValueOrDefault("pid") ?? "";
            if(value.Length != 9) return false;
            if(!int.TryParse(value, out num)) return false;
            return true;
        }

        static void Main(string[] args)
        {
            int count = 0;
            foreach (var passport in GetData())
            {
                if(IsPassportStrictlyValid(passport))
                    count++;
            }
            Console.WriteLine(count);
        }
    }
}
