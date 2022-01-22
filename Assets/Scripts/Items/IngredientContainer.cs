using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientContainer 
{
    public string containerName { get; protected set; }
    public float totalVolume { get; protected set; } // total volume of ingredients that the jug can hold
    public float currentlyUsedVolume { get; protected set; }

    public List<Ingredient> currentlyHolding { get; protected set; }

    public IngredientContainer(string containerName, float totalVolume)
    {
        this.containerName = containerName;
        this.totalVolume = totalVolume;

        currentlyHolding = new List<Ingredient>();
    }


    /// <summary>
    /// Adds an ingredient to the current container
    /// will only add upto the totalVolume of the container and no more
    /// </summary>
    /// <param name="ingredient"></param>
    public void AddIngredient(Ingredient ingredient)
    {
        float toAddAmount = ingredient.weight;
        if(totalVolume < toAddAmount + currentlyUsedVolume)
        {
            toAddAmount = totalVolume - currentlyUsedVolume;
            // Update the ingredients wieght
            ingredient.SetNewWeight(toAddAmount);
        }

        currentlyHolding.Add(ingredient);
        currentlyUsedVolume += toAddAmount;
    }
}
