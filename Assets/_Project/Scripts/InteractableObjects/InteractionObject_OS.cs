using System.Collections.Generic;
using UnityEngine;

namespace CoffeeBean
{

    [CreateAssetMenu(fileName = "New Interaction Item", menuName = "Coffee/Items/Interaction Item")]
    public class InteractionObject_OS : ScriptableObject
    {
        public string Name;

        public List<Tag_SO> Tags;
    }
}