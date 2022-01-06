using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2020.Day21;

public class Day21Solver : DaySolver
{
	private readonly List<Food> _foods;
	private readonly Lazy<Dictionary<Ingredient, Allergen>> _ingredients;

	public Day21Solver(string inputFilePath) : base(inputFilePath)
	{
		_foods = new();
		foreach (string line in InputLines)
		{
			(string str1, string str2) = line.SplitIntoTwo(" (contains ");
			str2 = str2.TrimEnd(')');
			HashSet<Ingredient> ingredients = str1.Split(' ').Select(str => new Ingredient(str)).ToHashSet();
			HashSet<Allergen> allergens = str2.Split(", ").Select(str => new Allergen(str)).ToHashSet();
			_foods.Add(new Food(ingredients, allergens));
		}
		_ingredients = new Lazy<Dictionary<Ingredient, Allergen>>(GenerateIngredientsToAllergensDictionary);
	}

	private Dictionary<Ingredient, Allergen> GenerateIngredientsToAllergensDictionary()
	{
		Dictionary<Allergen, HashSet<Ingredient>> possibleIngredients = new Dictionary<Allergen, HashSet<Ingredient>>();
		foreach (Food food in _foods)
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
			foreach (Food food in _foods)
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
					foreach (Allergen al in possibleIngredients.Keys.Where(a => a != allergen))
					{
						possibleIngredients[al].Remove(ingredient);
					}
				}
			}
		}
		Dictionary<Ingredient, Allergen> ingredients = new Dictionary<Ingredient, Allergen>();
		foreach (Food food in _foods)
		{
			foreach (Ingredient ingredient in food.Ingredients)
			{
				if (ingredients.ContainsKey(ingredient)) continue;
				ingredients[ingredient] = possibleIngredients.SingleOrDefault(p => p.Value.Single() == ingredient).Key;
			}
		}
		return ingredients;
	}

	public override string SolvePart1()
	{
		int count = 0;
		foreach (Food food in _foods)
		{
			count += food.Ingredients.Count(i => _ingredients.Value[i] == null);
		}
		return count.ToString();
	}

	public override string SolvePart2()
	{
		IEnumerable<Ingredient> dangerousIngredients = _ingredients.Value
			.Where(pair => pair.Value != null)
			.OrderBy(pair => pair.Value)
			.Select(pair => pair.Key);
		return string.Join(',', dangerousIngredients);
	}
}
