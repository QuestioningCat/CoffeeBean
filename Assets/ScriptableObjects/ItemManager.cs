using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    ItemSO itemData;

    
    private GameObject item_GO;

    private List<GameObject> item_GameObjects = new List<GameObject>();

    private int itemIndex = 0;

    public void UpdateItemData()
    {
        if(itemData != null)
        {
            this.transform.name = "Item - " + itemData.Name;

            if(item_GameObjects.Count > 0 || GetComponent<Transform>())
            {
                // this is only done to make sure we have a clean list before doing anything else.
                for(int i = 0; i < item_GameObjects.Count; i++)
                {
                    DestroyImmediate(item_GameObjects[i]);
                }
                item_GameObjects = new List<GameObject>();
            }

            foreach(GameObject go in itemData.ItemStates)
            {
                GameObject temp_GO = Instantiate(go, this.transform.position, Quaternion.identity, this.transform);
                item_GameObjects.Add(temp_GO);
                temp_GO.SetActive(false);
            }

            if (itemIndex < itemData.ItemStates.Count)
                item_GameObjects[itemIndex].SetActive(true);
            else
                item_GameObjects[0].SetActive(true);

        }
    }

    public void ChangeItemState(int index)
    {
        if(index < itemData.ItemStates.Count)
        {
            item_GameObjects[itemIndex].SetActive(false);
            item_GameObjects[index].SetActive(true);
            itemIndex = index;
            UpdateItemData();
        }
    }

    public ItemSO GetItemSOData() { return itemData; }
}
