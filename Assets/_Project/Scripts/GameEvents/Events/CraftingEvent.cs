using CoffeeBean;
using UnityEngine;

namespace CoffeeBean.Event
{
    [CreateAssetMenu(fileName = "New Crafting Event", menuName = "Game Events/DataPacket/Crafting Event")]
    public class CraftingEvent : BaseGameEvent<CraftingDataPacket> { }
}