using CoffeeBean;
using UnityEngine.Events;

namespace CoffeeBean.Event
{
    [System.Serializable] public class UnityItemStateChangeEvent : UnityEvent<ItemDataPacket> { }
}
