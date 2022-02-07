using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHitboxDataPacket
{
    public Item Item { get; protected set; }
    public Collider Hitbox { get; protected set; }

    public ItemHitboxDataPacket(Item item, Collider hitbox)
    {
        this.Item = item;
        this.Hitbox = hitbox;
    }
}
