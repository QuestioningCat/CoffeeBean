using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Is used to manage and update all interactable items in the game
/// has a reference to all children which both has a transform and an item controller on it
/// </summary>
public class StateBasedItemsManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> InteractableItems = new List<GameObject>();


    public void UpdateInteractableItems()
    {
        for(int i = 0; i < InteractableItems.Count; i++)
        {
            InteractableItems[i].GetComponent<ItemManager>()?.UpdateItemData();
        }
    }

    public void UpdateInteractableItemsList()
    {
        InteractableItems = new List<GameObject>();

        for(int i = 0; i < this.transform.childCount; i++)
        {
            if(transform.GetChild(i).name != this.name)
            {
                InteractableItems.Add(transform.GetChild(i).gameObject);
            }
        }
    }  
    

    public void RemoveGameObjectFromList(GameObject gameObject)
    {
        InteractableItems.Remove(gameObject);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(StateBasedItemsManager))]
class StateBasedItemsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var stateBasedItemsManager = (StateBasedItemsManager)target;
        if(stateBasedItemsManager == null)
            return;

        if(GUILayout.Button("Build interactable items list"))
        {
            // check to see if there have been any new Interactable Items added to this GameObjects children.
            stateBasedItemsManager.UpdateInteractableItemsList();
        }


        if(GUILayout.Button("Update all interactable items data"))
        {
            // Update the data/ modles for all Interactable Items that are a child of this GameObject.
            stateBasedItemsManager.UpdateInteractableItems();
            }
        
        


        DrawDefaultInspector();
    }
}
#endif