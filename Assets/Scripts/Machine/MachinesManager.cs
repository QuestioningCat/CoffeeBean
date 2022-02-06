using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinesManager : MonoBehaviour
{
    private Dictionary<int, Machine> machinesDictionary;

    private void Awake()
    {
        machinesDictionary = new Dictionary<int, Machine>();
    }

    public void RegisterNewItem(Machine machines)
    {
        foreach(int ID in machinesDictionary.Keys)
        {
            if(machinesDictionary[ID] == machines)
            {
                // If this Item has already been registerd
                // then just send back its ID so it can update itselft
                machines.UpdateMachineID(ID);
            }
        }

        int index = machinesDictionary.Count;
        machinesDictionary.Add(index, machines);
        // This Item has now been registed and we can send back the ID of the Item.
        machines.UpdateMachineID(index);
    }
}
