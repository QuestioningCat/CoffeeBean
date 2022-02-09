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
            //Debug.Log("Added new grinder to list, Total is: " + grinders.Count);
        }
    }

    public void PlayerClickedOnHitBox(ItemHitboxDataPacket dataPacket)
    {
        Grinder grinder = dataPacket.Hitbox.transform.GetComponent<Grinder>();
        if (dataPacket.Item == null && grinder != null)
        {
            // The player has clicked on a grinder but is not holding anything.
            // Find out what part of the machine the player has clicked on.
            AttachmentPoint attachmentPoint = grinder.GetAttachmentPoin(dataPacket.Hitbox);
            if(attachmentPoint != null)
            {
                // if there is an item attached to that part.
                if (attachmentPoint.AttachedItem != null)
                {
                    ItemHitboxDataPacket dp = new ItemHitboxDataPacket(attachmentPoint.AttachedItem, dataPacket.Hitbox, dataPacket.ClickedHand);
                    attachmentPoint.UpdateAttachedItem(null);
                    // raise the player pickup event.
                    onPlayerPickedUpItem.Raise(dp);
                }
            }
        }
        else
        {
            if(grinder != null && grinders.Contains(grinder) && dataPacket.Item.GetItemStateIndex() == 0)
            {
                AttachmentPoint attachmentPoint = grinder.GetAttachmentPoin(dataPacket.Hitbox);
                if(attachmentPoint != null && attachmentPoint.AttachedItem == null)
                {
                    // raise the attach event
                    onItemAttached.Raise(dataPacket.Item);
                    // Update the attachmentPoints attachedItem.
                    attachmentPoint.UpdateAttachedItem(dataPacket.Item);
                    // move the Item to the attachment point
                    dataPacket.Item.transform.position = attachmentPoint.AttachPoint.position;
                    dataPacket.Item.transform.rotation = attachmentPoint.AttachPoint.rotation;
                    // tell the grinder to start grinding cofee
                    grinder.GrindCoffee(dataPacket.Item);
                }
            }
        }
    }
}
