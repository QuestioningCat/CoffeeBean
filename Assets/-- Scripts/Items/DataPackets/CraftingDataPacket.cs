using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingDataPacket
{
    public TwoCompoentRecipes_SO TwoComponentRecipe { get; protected set; }
    public OneComponentRecipes_OS OneComponentRecipe { get; protected set; }

    public Item FirstComponent { get; protected set; }
    public Item SecondComponent { get; protected set; }


    public CraftingDataPacket(Item firstComponent, Item secondComponent, TwoCompoentRecipes_SO recipe)
    {
        this.FirstComponent = firstComponent;
        this.SecondComponent = secondComponent;
        this.TwoComponentRecipe = recipe;
    }

    public CraftingDataPacket(Item component, OneComponentRecipes_OS recipe)
    {
        this.FirstComponent = component;
        this.OneComponentRecipe = recipe;
    }
}
