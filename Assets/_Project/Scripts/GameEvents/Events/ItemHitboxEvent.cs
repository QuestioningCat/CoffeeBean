using CoffeeBean;
using UnityEngine;

namespace CoffeeBean.Event
{

    [CreateAssetMenu(fileName = "New Item Hitbox Event", menuName = "Game Events/DataPacket/Item Hitbox Event")]
    public class ItemHitboxEvent : BaseGameEvent<ItemHitboxDataPacket> { }
}