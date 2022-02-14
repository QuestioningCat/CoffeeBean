using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hand { LeftHand, RightHand, NoHands };

[RequireComponent(typeof(BoxCollider)), RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    [Header("Scriptable Object")]
    [Tooltip("The scriptable object template")]
    [SerializeField] private ItemSO itemData;
    
    [Tooltip("Every Recipes this Item can be used to make")]
    [SerializeField] private List<TwoCompoentRecipes_SO> recipes;

    [Header("Events")]
    [SerializeField] private ItemEvent onNewItemCreated;

    // The item ID for the Item this is managing.
    private int ID = -1;
    private int currentStateIndex = -1;

    private Hand currentHand = Hand.NoHands;

    private bool registered = false;

    private GameObject currentItem = null;

    public void UpdateItemID(int newID) { ID = newID; }
    public int GetItemID() { return ID; }
    public int GetItemStateIndex() { return currentStateIndex; }
    public ItemSO GetItemSOData() { return itemData; }


    private void Start()
    {
        onNewItemCreated.Raise(this);
        ChangeItemState(0);
    }


    private void OnEnable()
    {
        if(ID < 0 && registered)
            onNewItemCreated.Raise(this);
    }


    /// <summary>
    /// Will return the current hand the Item is in
    /// </summary>
    /// <returns></returns>
    public Hand GetHandItemIsIn() { return currentHand; }

    /// <summary>
    /// Will set the current hand for the item
    /// </summary>
    /// <param name="newHand"></param>
    public void UpdateCurrentHand(Hand newHand) => currentHand = newHand;

    public void ChangeItemState(int stateIndex)
    {
        if(currentStateIndex != stateIndex && stateIndex < itemData.ItemStates.Count)
        {
            if(currentItem == null)
            {
                currentItem = this.transform.GetChild(0).gameObject;
            }

            //TODO: ADD this item to a list of stored items so they can be loaded in only once and not multiple times.
            Destroy(currentItem);
            currentItem = Instantiate(itemData.ItemStates[stateIndex], this.transform.position, currentItem.transform.rotation, this.transform);


            this.transform.name = "Item - " + itemData.Name + ":" + ID;
            currentStateIndex = stateIndex;
        }
    }

    /// <summary>
    /// Checks to see if this item and the given item belong to a recipe
    /// </summary>
    /// <param name="otherItem"></param>
    /// <returns> Returns the recipes these 2 items belong too </returns>
    public TwoCompoentRecipes_SO IsValidRecipeCombination(Item otherItem, bool handRecipe)
    {
        if(recipes.Count == 0)
            return null;

        if(otherItem == null)
            return null;
        
        foreach(TwoCompoentRecipes_SO recipe in recipes)
        {
            //Debug.Log("DING: " + otherItem.itemData.type

            if( (recipe.ComponentOne == this.itemData.type && recipe.ComponentOneStartState == this.currentStateIndex) ||
                (recipe.ComponentOne == otherItem.itemData.type && recipe.ComponentOneStartState == otherItem.currentStateIndex) )
            {

                if( ((recipe.ComponentTwo == this.itemData.type && recipe.ComponentTwoStartState == this.currentStateIndex) || 
                    (recipe.ComponentTwo == otherItem.itemData.type && recipe.ComponentTwoStartState == otherItem.currentStateIndex)) )
                {
                    if ( recipe.HandRecipe == handRecipe)
                        return recipe;
                }
            }
        }
        return null;
    }
}

