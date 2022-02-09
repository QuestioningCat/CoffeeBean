using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderController : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private ItemEvent onItemAttached;
    [SerializeField] private ItemHitboxEvent onPlayerPickedUpItem;

    private List<Grinder> grinders;

    private void Awake()
    {
        grinders = new List<Grinder>();
    }

    public void RegisterNewGrinder(Grinder grinder)
    {
        if (!grinders.Contains(grinder))
        {
            grinders.Add(grinder);
        }
    }

    public void PlayerClickedOnHitBox(ItemHitboxDataPacket dataPacket)
    {
        Grinder grinder = dataPacket.Hitbox.transform.GetComponentInParent<Grinder>();
        if (dataPacket.Item == null && grinder != null)
        {
            // The player has clicked on a grinder but is not holding anything.
            // Find out what part of the machine the player has clicked on.
            AttachmentPoint ap = grinder.GetAttachmentPoint(dataPacket.Hitbox);
            if(ap != null)
            {
                // if there is an item attached to that part.
                if (ap.GetAttachedItem() != null)
                {
                    ItemHitboxDataPacket dp = new ItemHitboxDataPacket(ap.GetAttachedItem(), dataPacket.Hitbox, dataPacket.ClickedHand);
                    ap.UpdateAttachedItem(null);
                    // raise the player pickup event.
                    onPlayerPickedUpItem.Raise(dp);
                }
            }
        }
        else
        {
            if(grinder != null && grinders.Contains(grinder) && dataPacket.Item.GetItemStateIndex() == 0)
            {
                AttachmentPoint attachmentPoint = grinder.GetAttachmentPoint(dataPacket.Hitbox);
                if(attachmentPoint != null && attachmentPoint.GetAttachedItem() == null)
                {
                    // raise the attach event
                    onItemAttached.Raise(dataPacket.Item);
                    // Update the attachmentPoints attachedItem.
                    attachmentPoint.UpdateAttachedItem(dataPacket.Item);
                    // move the Item to the attachment point
                    dataPacket.Item.transform.position = attachmentPoint.transform.position;
                    dataPacket.Item.transform.rotation = attachmentPoint.transform.rotation;
                    // tell the grinder to start grinding cofee
                    grinder.GrindCoffee(dataPacket.Item);
                }
            }
        }
    }
}
