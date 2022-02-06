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
        if (itemData.Item.GetItemID() < itemsDictionary.Count)
        {
            //Debug.Log("Updated item: " + itemData.Item.GetItemID() +" To new state of: " + itemData.NewStateIndex);
            itemsDictionary[itemData.Item.GetItemID()].ChangeItemState(itemData.NewStateIndex);
        }
    }
}
