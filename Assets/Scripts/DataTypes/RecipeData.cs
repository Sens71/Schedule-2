using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipes", menuName = "MyData/Recipes")]
public class RecipeData : ScriptableObject
{
    public Recipe[] recipes;
}
[Serializable]
public class Recipe
{
    public RecipeSlot[] ingredients;
    public RecipeSlot result;
    public ClockTime duration;
}

[Serializable]
public class RecipeSlot
{
    public ItemData item;
    public int amount;
}