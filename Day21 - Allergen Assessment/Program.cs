using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Linq;
using StringExtensionMethods;

namespace Day21
{
    class Program
    {
        static string[] ReadLines(string path)
        {
            FileStream filestream = null;
            try
            {
                filestream = new FileStream(path, FileMode.Open);
                using (StreamReader reader = new StreamReader(filestream))
                {
                    return reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
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

        static List<Food> GetInput(string path)
        {
            string[] lines = ReadLines(path);
            List<Food> list = new List<Food>();
            foreach (string line in lines)
            {
                var (str1, str2) = line.SplitIntoTwo(" (contains ");
                str2 = str2.Trim(')');
                HashSet<Ingredient> ingredients = str1.Split(' ').Select(str => new Ingredient(str)).ToHashSet();
                HashSet<Allergen> allergens = str2.Split(", ").Select(str => new Allergen(str)).ToHashSet();

                list.Add(new Food(ingredients, allergens));
            }
            return list;
        }

        static void Main(string[] args)
        {
            List<Food> foods = GetInput("input.txt");
            Dictionary<Allergen, HashSet<Ingredient>> possibleIngredients = new Dictionary<Allergen, HashSet<Ingredient>>();
            foreach (Food food in foods)
            {
                foreach (Allergen allergen in food.Allergens)
                {
                    if (possibleIngredients.ContainsKey(allergen))
                    {
                        possibleIngredients[allergen].UnionWith(food.Ingredients);
                    }
                    else
                    {
                        possibleIngredients[allergen] = new HashSet<Ingredient>(food.Ingredients);
                    }
                }
            }
            while (!possibleIngredients.All(pair => pair.Value.Count == 1))
            {
                foreach (Food food in foods)
                {
                    foreach (Allergen allergen in food.Allergens)
                    {
                        possibleIngredients[allergen].IntersectWith(food.Ingredients);
                    }
                }
                foreach (Allergen allergen in possibleIngredients.Keys)
                {
                    if (possibleIngredients[allergen].Count == 1)
                    {
                        Ingredient ingredient = possibleIngredients[allergen].Single();
                        foreach (Allergen al in possibleIngredients.Keys.Where(a=>a!=allergen))
                        {
                            possibleIngredients[al].Remove(ingredient);
                        }
                    }
                }
            }
            Dictionary<Ingredient, Allergen> ingredients = new Dictionary<Ingredient, Allergen>();
            foreach (Food food in foods)
            {
                foreach (Ingredient ingredient in food.Ingredients)
                {
                    if (ingredients.ContainsKey(ingredient)) continue;
                    ingredients[ingredient] = possibleIngredients.SingleOrDefault(p => p.Value.Single() == ingredient).Key;
                }
            }
            int count = 0;
            foreach (Food food in foods)
            {
                count += food.Ingredients.Count(i => ingredients[i] == null);
            }
            Console.WriteLine($"Part1: {count}");
            string part2 = string.Join(',', ingredients.Where(pair => pair.Value != null).OrderBy(pair => pair.Value).Select(pair => pair.Key));
            System.Console.WriteLine($"Part2: \"{part2}\"");
        }
    }
}
