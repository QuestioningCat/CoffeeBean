using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Machines", menuName = "Coffee/Machines/Espresso Machine")]
public class EspressoMachine_SO : ScriptableObject
{
    public string Name;

    public List<Tag_SO> Tags;
}
