using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for keeping track of all Items within the running game
// when a new Item gets created it must first register itself with the ItemsManager to get its ID.
// this keeps all ID dynamic and means I do not have to deal with it XD
public class ItemsManager : MonoBehaviour
{
    private Dictionary<int, Item> itemsDictionary;

    private void Awake()
    {
        itemsDictionary = new Dictionary<int, Item>();
    }

    public void RegisterNewItem(Item item)
    {
        foreach(int ID in itemsDictionary.Keys)
        {
            if ( itemsDictionary[ID] == item)
            {
                // If this Item has already been registerd
                // then just send back its ID so it can update itselft
                item.UpdateItemID(ID);
            }    
        }

        int index = itemsDictionary.Count;
        itemsDictionary.Add(index, item);
        // This Item has now been registed and we can send back the ID of the Item.
        item.UpdateItemID(index);
    }


    public void UpdateItemStateToGivenState(ItemDataPacket itemData)
    {
        if(itemData.Item.GetItemID() > itemsDictionary.Count)
        { 
            return; 
        }

        //Debug.Log("Updated item: " + itemData.Item.GetItemID() +" To new state of: " + itemData.NewStateIndex);
        itemsDictionary[itemData.Item.GetItemID()].ChangeItemState(itemData.NewStateIndex);
    }

    public void UpdateItemStateToGivenState(Item item, int newState)
    {
        if(item.GetItemID() == -1)
        {
            return;
        }
        item.ChangeItemState(newState);
    }


    public void NewItemCrafted(CraftingDataPacket dataPacket)
    {
        // place all items into it's resultent state
        Item item1 = dataPacket.FirstComponent;
        if(dataPacket.TwoComponentRecipe != null)
        {
            Item item2 = dataPacket.SecondComponent;

            if(item1.HasTag(dataPacket.TwoComponentRecipe.ComponentOne.name))
            {
                UpdateItemStateToGivenState(item1, dataPacket.TwoComponentRecipe.ComponentOneResultState);
                UpdateItemStateToGivenState(item2, dataPacket.TwoComponentRecipe.ComponentTwoResultState);
            }
            else
            {
                UpdateItemStateToGivenState(item1, dataPacket.TwoComponentRecipe.ComponentTwoResultState);
                UpdateItemStateToGivenState(item2, dataPacket.TwoComponentRecipe.ComponentOneResultState);
            }
        }
        else if (dataPacket.OneComponentRecipe != null)
        {
            
            UpdateItemStateToGivenState(item1, dataPacket.OneComponentRecipe.ComponentOneResultState);
        }
    }
}
