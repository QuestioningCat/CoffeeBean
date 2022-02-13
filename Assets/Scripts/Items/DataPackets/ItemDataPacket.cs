using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataPacket
{
    public Item Item { get; protected set; }
    public int NewStateIndex { get; protected set; }

    public ItemDataPacket(Item item, int newStateIndex)
    {
        this.Item = item;
        this.NewStateIndex = newStateIndex;
    }
}
