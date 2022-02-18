using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoffeeEquipmentComponentes { Portafilter, MilkJug, MilkCarton, CoffeeBag, Cup};

[CreateAssetMenu(fileName = "Coffee Component Item", menuName = "Coffee/Items/Coffee Component Item")]
public class ItemSO : ScriptableObject
{
    public string Name;

    public List<Tag_SO> Tags;

    public CoffeeEquipmentComponentes Type;

    // all items state are loaded at the same time.
    // but only one is enabled
    // this is done to prevent objects being created and destroyed over and over again,
    // in the atempt to improive memeory performance.
    // allthough on this scale I do not think it matters.
    // and haveing all varients loaded in memeory at the same time could cause more harm them good.
    public List<GameObject> ItemStates;

}
