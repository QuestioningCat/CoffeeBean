using CoffeeBean.Event;
using System.Collections.Generic;
using UnityEngine;
using CoffeeBean.Tag;

namespace CoffeeBean.Machine
{


    public class Grinder : MonoBehaviour, IInteractable
    {
        [Header("Template Scriptable Object")]
        [SerializeField] private Grinder_SO grinder_SO;

        [Header("Events")]
        [SerializeField] private GrinderEvent onNewGrinderCreated;
        [SerializeField] private ItemEvent onItemAttached;
        [SerializeField] private ItemHitboxEvent onPlayerPickedUpItem;


        [SerializeField] private ItemStateChangeEvent onGrindCoffeeIntoGrinder;

        [Header("Attachment Points")]
        // Possible improvment would be to generate this list at runtime to further reduce complexity for the designers.
        [SerializeField] private List<AttachmentPoint> attachmentPoints = new List<AttachmentPoint>();


        /// <summary>
        /// Grinds coffee from the hopper into the portafilter
        /// </summary>
        /// <param name="portafilter"></param>
        public void GrindCoffee(Item portafilter)
        {
            if(portafilter.GetItemStateIndex() != 0)
                return;
            onGrindCoffeeIntoGrinder.Raise(new ItemDataPacket(portafilter, onGrindCoffeeIntoGrinder.NewState));
        }
        public bool GetTag(string tag)
        {

            if(tag == "" || tag == null)
                return false;

            foreach(Tag_SO t in grinder_SO.Tags)
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

        /// <summary>
        /// Returns the attachment point for the given collider
        /// </summary>
        /// <param name="collider"></param>
        /// <returns></returns>
        public AttachmentPoint GetAttachmentPoint(Collider collider)
        {
            foreach(AttachmentPoint attachmentPoint in attachmentPoints)
            {
                if(attachmentPoint.GetHitBox() == collider)
                    return attachmentPoint;
            }
            return null;
        }

        private void Start()
        {
            // Register the grind with it's controller
            onNewGrinderCreated.Raise(this);
        }


        public void Interact(ItemHitboxDataPacket dataPacket)
        {
            if(dataPacket.Item == null)
            {

                // The player has clicked on a grinder but is not holding anything.
                // Find out what part of the machine the player has clicked on.
                AttachmentPoint ap = GetAttachmentPoint(dataPacket.Hitbox);
                if(ap == null)
                {
                    return;
                }
                // if there is an item attached to that part.
                if(ap.GetAttachedItem() == null)
                {
                    return;
                }

                ItemHitboxDataPacket dp = new ItemHitboxDataPacket(ap.GetAttachedItem(), dataPacket.Hitbox, dataPacket.ClickedHand);
                ap.UpdateAttachedItem(null);
                // raise the player pickup event.
                onPlayerPickedUpItem.Raise(dp);
            }
            else if(dataPacket.Item != null)
            {
                if(!dataPacket.Item.HasTag("Portafilter"))
                    return;

                AttachmentPoint attachmentPoint = GetAttachmentPoint(dataPacket.Hitbox);
                if(attachmentPoint == null && attachmentPoint.GetAttachedItem() != null)
                {
                    return;
                }

                // raise the attach event
                onItemAttached.Raise(dataPacket.Item);
                // Update the attachmentPoints attachedItem.
                attachmentPoint.UpdateAttachedItem(dataPacket.Item);
                // move the Item to the attachment point
                dataPacket.Item.transform.position = attachmentPoint.transform.position;
                dataPacket.Item.transform.rotation = attachmentPoint.transform.rotation;
                // tell the grinder to start grinding cofee
                GrindCoffee(dataPacket.Item);
            }
        }
    }

}