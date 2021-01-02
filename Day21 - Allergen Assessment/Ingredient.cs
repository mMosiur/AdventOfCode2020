namespace Day21
{
    public record Ingredient
    {
        public string Name { get; }

        public Ingredient(string name)
        {
            Name = name;
        }

        public static implicit operator Ingredient(string str)
        {
            return new Ingredient(str);
        }

        public static implicit operator string(Ingredient Ingredient)
        {
            return Ingredient.ToString();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}