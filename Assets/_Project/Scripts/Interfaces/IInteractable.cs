using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeBean
{
    public interface IInteractable
    {
        void Interact(ItemHitboxDataPacket datapacket);
    }
}
