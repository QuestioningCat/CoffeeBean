using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeBean.Machine
{

    [CreateAssetMenu(fileName = "Machines", menuName = "Coffee/Machines/Grinder")]
    public class Grinder_SO : ScriptableObject
    {
        public string Name;
        public int HopperCapacity;

        public List<Tag_SO> Tags;
    }
}
