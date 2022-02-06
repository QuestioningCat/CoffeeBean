using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MachineType { Grinder, EsspressoMachine };

[CreateAssetMenu(fileName = "Coffee Machine", menuName = "Machines/Coffee Machine")]
public class Machine_SO : ScriptableObject
{
    public string Name;
    public MachineType MachineType;
    public int MaxCoffeeCapacity;
    public int MaxWaterCapacity;
    public int MaxTempracture;
}
