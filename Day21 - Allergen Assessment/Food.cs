using System.Collections.Generic;

namespace AdventOfCode.Year2020.Day21;

public class Food
{
	public Food(HashSet<Ingredient> ingredients, HashSet<Allergen> allergens)
	{
		Ingredients = ingredients;
		Allergens = allergens;
	}

	public HashSet<Ingredient> Ingredients { get; }
	public HashSet<Allergen> Allergens { get; }

}
