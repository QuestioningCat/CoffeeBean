using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour
{
    [Header("Scriptable Object")]
    [Tooltip("The scriptable object which this scrip will be responcible for")]
    [SerializeField]
    ItemSO itemData;

    [Header("Events")]
    [SerializeField]
    private ItemEvent onNewItemCreated;

    // The item ID for the Item this is managing.
    private int ID = -1;

    private int currentIndex = -1;

    private GameObject currentItem = null;

    public void UpdateItemID(int newID) { ID = newID; }

    public ItemSO GetItemSOData() { return itemData; }


    private void OnEnable()
    {
        onNewItemCreated.Raise(this);
        ChangeItemState(0);
    }

    public void ChangeItemState(int index)
    {

            if(currentIndex != index && index < itemData.ItemStates.Count)
            {
                if(currentItem == null)
                {
                    currentItem = this.transform.GetChild(0).gameObject;
                }

                //TODO: ADD this item to a list of stored items so they can be loaded in only once and not multiple times.
                Destroy(currentItem);

                currentItem = Instantiate(itemData.ItemStates[index], this.transform.position, Quaternion.identity, this.transform);


                this.transform.name = "Item - " + itemData.Name + ":" + ID;
                currentIndex = index;
            }
    }
}
