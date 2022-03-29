using System.Collections.Generic;
using UnityEngine;
using CoffeeBean.Event;

namespace CoffeeBean.Machine
{
    public class EspressoMachine : MonoBehaviour, IInteractable
    {
        [Header("Scriptable Object")]
        [SerializeField] private EspressoMachine_SO espressoMachine_SO;

        [Space(10)]
        [SerializeField] private List<AttachmentPoint> allAttachmentPoints = new List<AttachmentPoint>();

        [Header("Events")]
        [SerializeField] private ItemEvent onItemAttached;
        [SerializeField] private ItemHitboxEvent onPlayerPickedUpItem;
        [SerializeField] private CraftingEvent onNewItemCrafted;
        [SerializeField] private ItemStateChangeEvent onSteamMilk;

        public AttachmentPoint GetAttachmentPoin(Collider collider)
        {
            foreach(AttachmentPoint attachmentPoint in allAttachmentPoints)
            {
                if(attachmentPoint == null)
                    continue;

                if(attachmentPoint.GetHitBox() == collider)
                    return attachmentPoint;
            }
            return null;
        }

        public bool GetTag(string tag)
        {

            if(tag == "" || tag == null) return false;

            foreach(Tag_SO t in espressoMachine_SO.Tags)
            {
                if(t == null)
                    continue;
                if(t.name == tag)
                {
                    return true;
                }
            }


            return false;
        }

        public void Interact(ItemHitboxDataPacket dataPacket)
        {
            if(dataPacket.Item == null)
            {
                // The player has clicked on a grinder but is not holding anything.
                // Find out what part of the machine the player has clicked on.
                AttachmentPoint attachmentPoint = GetAttachmentPoin(dataPacket.Hitbox);
                if(attachmentPoint == null) return;
                // if there is an item attached to that part.
                if(attachmentPoint.GetAttachedItem() == null) return;

                ItemHitboxDataPacket dp = new ItemHitboxDataPacket(attachmentPoint.GetAttachedItem(), dataPacket.Hitbox, dataPacket.ClickedHand);
                attachmentPoint.UpdateAttachedItem(null);
                // raise the player pickup event.
                onPlayerPickedUpItem.Raise(dp);
            }
            else
            {
                AttachmentPoint attachmentPoint = GetAttachmentPoin(dataPacket.Hitbox);

                if(attachmentPoint == null) return;

                if(attachmentPoint.GetAttachedItem() != null) return;

                switch(attachmentPoint.GetAttachmentType().name)
                {
                    case "GroupHead":
                        if(!dataPacket.Item.HasTag("Portafilter")) break;
                        if(dataPacket.Item.GetItemStateIndex() == 1)
                            AttachItem(dataPacket.Item, attachmentPoint);
                        break;

                    case "SteamWand":
                        if(!dataPacket.Item.HasTag("MilkJug")) break;
                        if(dataPacket.Item.GetItemStateIndex() == 1)
                        {
                            AttachItem(dataPacket.Item, attachmentPoint);
                            onSteamMilk.Raise(new ItemDataPacket(dataPacket.Item, 2));
                        }
                        break;

                    case "DripTray":
                        if(!dataPacket.Item.HasTag("Cup")) break;
                        if(dataPacket.Item.GetItemStateIndex() == 0)
                            AttachItem(dataPacket.Item, attachmentPoint);
                        break;
                }

            }
        }

        /// <summary>
        /// Attached the given Item to the given AttachmentPoint
        /// </summary>
        /// <param name="item"></param>
        /// <param name="attachmentPoint"></param>
        private void AttachItem(Item item, AttachmentPoint attachmentPoint)
        {
            // raise the attach event
            onItemAttached.Raise(item);
            // Update the attachmentPoints attachedItem.
            attachmentPoint.UpdateAttachedItem(item);
            // move the Item to the attachment point
            item.transform.position = attachmentPoint.transform.position;
            item.transform.rotation = attachmentPoint.transform.rotation;
            // Check to see it we have made a recipe
            Item pairItem = attachmentPoint.GetAttachmentPointPair()?.GetAttachedItem();
            TwoCompoentRecipes_SO recipe = item.IsValidTwoComponentRecipeCombination(pairItem, false);
            if(recipe == null) return;
            // The combination is a Recipe Match.
            // Raied the Item Crafted Event.
            onNewItemCrafted.Raise(new CraftingDataPacket(item, pairItem, recipe));

        }
    }
}