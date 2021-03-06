using CoffeeBean;
using UnityEngine;

namespace CoffeeBean.Event
{

    [CreateAssetMenu(fileName = "New Item State Change", menuName = "Game Events/DataPacket/Item State Change")]
    public class ItemStateChangeEvent : BaseGameEvent<ItemDataPacket>
    {
        public int NewState;
    }

}