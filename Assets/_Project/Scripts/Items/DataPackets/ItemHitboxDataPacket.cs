using UnityEngine;

namespace CoffeeBean
{
    public class ItemHitboxDataPacket
    {
        public Item Item { get; protected set; }
        public Collider Hitbox { get; protected set; }
        public Hand ClickedHand { get; protected set; }

        public ItemHitboxDataPacket(Item item, Collider hitbox, Hand clickedHand = Hand.NoHands)
        {
            this.Item = item;
            this.Hitbox = hitbox;
            this.ClickedHand = clickedHand;
        }

    }

}