using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
    public struct OldPasswordPolicy
    {
        public char letter;
        public int minCount;
        public int maxCount;

        public OldPasswordPolicy(char letter, int num1, int num2)
        {
            this.letter = letter;
            minCount = num1;
            maxCount = num2;
        }

        public bool DoesMatch(string password)
        {
            char search = letter;
            int count = password.Count(c => c == search);
            if(count < minCount) return false;
            if(count > maxCount) return false;
            return true;
        }
    }

    public struct NewPasswordPolicy
    {
        public char letter;
        public int firstPos;
        public int secondPos;

        public NewPasswordPolicy(char letter, int num1, int num2)
        {
            this.letter = letter;
            firstPos = num1;
            secondPos = num2;
        }

        public bool DoesMatch(string password)
        {
            if(password.Length < firstPos) return false;
            int count = 0;
            if(password[firstPos-1] == letter) count++;
            if(secondPos <= password.Length)
            {
                if(password[secondPos-1] == letter) count++;
            }
            return count == 1;
        }
    }

    class Program
    {
        static IEnumerable<Tuple<NewPasswordPolicy, string>> GetData()
        {
            using (StreamReader stream = new("input.txt"))
            {
                string line;
                while(!string.IsNullOrEmpty(line = stream.ReadLine()))
                {
                    var segments = line.Split(' ');
                    int[] nums = segments[0].Split('-').Select(str => int.Parse(str)).ToArray();
                    NewPasswordPolicy policy = new NewPasswordPolicy(segments[1][0], nums[0], nums[1]);
                    yield return new Tuple<NewPasswordPolicy,string>(policy, segments[2]);
                }
            }
        }

        static void Main(string[] args)
        {
            int count = 0;
            foreach(var tup in GetData())
            {
                if(tup.Item1.DoesMatch(tup.Item2))
                    count++;
            }
            Console.WriteLine(count);
        }
    }
}
