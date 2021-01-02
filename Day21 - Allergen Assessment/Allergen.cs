using System;

namespace Day21
{
    public record Allergen : IComparable
    {
        public string Name { get; }

        public Allergen(string name)
        {
            Name = name;
        }

        public static implicit operator Allergen(string str)
        {
            return new Allergen(str);
        }

        public static implicit operator string(Allergen allergen)
        {
            return allergen.ToString();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object obj)
        {
            Allergen other = (Allergen)obj;
            return Name.CompareTo(other.Name);
        }
    }
}