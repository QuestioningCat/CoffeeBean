using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is a Scriptable Object that takes in 2 component parts and results in a new Item
/// </summary>
[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipes/2 Component Recipe")]
public class TwoCompoentRecipes_SO : ScriptableObject
{
    // can this recipe be performed in a machine or the players hands?
    public bool HandRecipe;

    // the types of Items needed for this recipe;
    public CoffeeEquipmentComponentes ComponentOne;
    // States the Components before crafting is allowed - Negative value means ingore
    public int ComponentOneStartState;
    // States of the components after crafting - Negative value means ignore
    public int ComponentOneResultState;
    public CoffeeEquipmentComponentes ComponentTwo;
    // States the Components before crafting is allowed - Negative value means ingore
    public int ComponentTwoStartState;
    // States of the components after crafting - Negative value means ignore
    public int ComponentTwoResultState;


}
