using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingDataPacket
{
    public TwoCompoentRecipes_SO Recipe { get; protected set; }

    public Item FirstComponent { get; protected set; }
    public Item SecondComponent { get; protected set; }


    public CraftingDataPacket(Item firstComponent, Item secondComponent, TwoCompoentRecipes_SO recipe)
    {
        this.FirstComponent = firstComponent;
        this.SecondComponent = secondComponent;
        this.Recipe = recipe;
    }
}
