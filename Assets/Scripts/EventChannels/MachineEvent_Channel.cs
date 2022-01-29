using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MachineEvent_Channel", menuName ="Channels/Machine Event")]
public class MachineEvent_Channel : ScriptableObject
{
    // HashSets can only have 1 copy of of each item in the list
    HashSet<GameObject> _Listeners = new HashSet<GameObject>();



}
