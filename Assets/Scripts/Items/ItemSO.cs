using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoffeeComponentItem", menuName = "Items/CoffeeComponentItem")]
public class ItemSO : ScriptableObject
{
    public string Name;

    public CoffeeEquipmentComponentes type;

    // all items state are loaded at the same time.
    // but only one is enabled
    // this is done to prevent objects being created and destroyed over and over again,
    // in the atempt to improive memeory performance.
    // allthough on this scale I do not think it matters.
    // and haveing all varients loaded in memeory at the same time could cause more harm them good.
    public List<GameObject> ItemStates;

}
