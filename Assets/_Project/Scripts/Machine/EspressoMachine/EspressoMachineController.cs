using CoffeeBean.Event;
using CoffeeBean;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeBean.Machine
{
    public class EspressoMachineController : MonoBehaviour
    {

        private List<EspressoMachine> espressoMachines;

        [Header("Event")]
        [SerializeField] private ItemEvent onItemAttached;
        [SerializeField] private ItemHitboxEvent onPlayerPickedUpItem;
        [SerializeField] private CraftingEvent onNewItemCrafted;
        [SerializeField] private ItemStateChangeEvent onSteamMilk;

        private void Awake()
        {
            espressoMachines = new List<EspressoMachine>();
        }

        /// <summary>
        /// Registers the given Espresso Machine to the list of all Espresso Machines
        /// </summary>
        /// <param name="machine"></param>
        public void RegisterNewEspressoMachine(EspressoMachine machine)
        {
            if(espressoMachines.Contains(machine))
            {
                return;
            }

            espressoMachines.Add(machine);
        }

        /// <summary>
        /// Gets called when the player has clicked on a hitbox, Collider
        /// </summary>
        /// <param name="dataPacket"></param>
        public void PlayerClickedOnHitBox(ItemHitboxDataPacket dataPacket)
        {
            EspressoMachine machine = dataPacket.Hitbox.transform.GetComponentInParent<EspressoMachine>();

            if(dataPacket.Item == null && machine != null)
            {
                // The player has clicked on a grinder but is not holding anything.
                // Find out what part of the machine the player has clicked on.
                AttachmentPoint attachmentPoint = machine.GetAttachmentPoin(dataPacket.Hitbox);
                if(attachmentPoint == null)
                {
                    return;
                }
                // if there is an item attached to that part.
                if(attachmentPoint.GetAttachedItem() == null)
                {
                    return;
                }

                ItemHitboxDataPacket dp = new ItemHitboxDataPacket(attachmentPoint.GetAttachedItem(), dataPacket.Hitbox, dataPacket.ClickedHand);
                attachmentPoint.UpdateAttachedItem(null);
                // raise the player pickup event.
                onPlayerPickedUpItem.Raise(dp);
            }
            else
            {

                if(machine == null && !espressoMachines.Contains(machine))
                {
                    return;
                }


                AttachmentPoint attachmentPoint = machine.GetAttachmentPoin(dataPacket.Hitbox);

                if(attachmentPoint == null)
                    return;

                switch(attachmentPoint.GetAttachmentType().name)
                {
                    case "GroupHead":
                        if(attachmentPoint == null && attachmentPoint.GetAttachedItem() != null)
                        {
                            break;
                        }

                        if(!dataPacket.Item.HasTag("Portafilter"))
                        {
                            break;
                        }

                        if(dataPacket.Item.GetItemStateIndex() == 1)
                            AttachItem(dataPacket.Item, attachmentPoint);

                        break;
                    case "SteamWand":
                        if(attachmentPoint == null && attachmentPoint.GetAttachedItem() != null)
                        {
                            break;
                        }

                        if(!dataPacket.Item.HasTag("MilkJug"))
                        {
                            break;
                        }

                        if(dataPacket.Item.GetItemStateIndex() == 1)
                        {
                            AttachItem(dataPacket.Item, attachmentPoint);
                            onSteamMilk.Raise(new ItemDataPacket(dataPacket.Item, 2));
                        }

                        break;
                    case "DripTray":

                        if(attachmentPoint == null && attachmentPoint.GetAttachedItem() != null)
                        {
                            break;
                        }

                        if(!dataPacket.Item.HasTag("Cup"))
                        {
                            break;
                        }

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
            if(recipe == null)
            {
                return;
            }
            // The combination is a Recipe Match.
            // Raied the Item Crafted Event.
            onNewItemCrafted.Raise(new CraftingDataPacket(item, pairItem, recipe));

        }
    }
}