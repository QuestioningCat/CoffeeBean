using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType { Air, Milk, Coffee }

/// <summary>
/// Everything that can be made in coffee bean is made up of ingerdients
/// This could be coffee, milk, water and even air (needed for the milk steaming)
///
/// Data container contains:
/// - what type the ingredient is.
/// - how much of it there is.
/// - how hot it is.
/// - the quality.
/// </summary>
public class Ingredient : MonoBehaviour
{
    

    public IngredientType type { get; protected set; } // the type of the ingredient

    public float weight { get; protected set; } // the weight in grams of the given ingredient;

    public float currentTemp { get; protected set; } // current temp of the ingredient in C

    public int quality { get; protected set; } // The Quality of the ingredient from 0 to 100

    public Ingredient(IngredientType type, float weight = 0f)
    {
        this.type = type;
        this.weight = weight;
        this.currentTemp = 20f;
        this.quality = 50;
    }

    public Ingredient(IngredientType type, float weight, float currentTemp, int quality)
    {
        this.type = type;
        this.weight = weight;
        this.currentTemp = currentTemp;
        this.quality = quality;
    }

    /// <summary>
    /// Sets the current weight to the new weight
    /// </summary>
    /// <param name="amount"></param>
    public void SetNewWeight(float amount)
    {
        weight = amount;
        // TODO:: trigger a callback to let other know that the weight has changed
    }
}
