using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Only one will exist in the world, for now. So I am going to be lazy and just put the controller on the actual object.
public class DeliveryController : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private ItemEvent onItemDelivered;
    [SerializeField] private ItemEvent onItemAttached;

    [SerializeField] private Tag_SO deliveryTag;

    [SerializeField]
    private AttachmentPoint attachmentPoint;

    public void PlayerClickedOnHitBox(ItemHitboxDataPacket dataPacket)
    {

        if(dataPacket.Item == null || dataPacket.Hitbox.GetComponentInParent<DeliveryController>() == null)
            return;

        if(!dataPacket.Item.HasTag("Cup"))
            return;

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

        // tell the other scripst that an Item has been placed on me.
        onItemDelivered.Raise(dataPacket.Item);
    }

    public bool GetTag(string tag)
    {

        if(tag == "" || tag == null || deliveryTag == null)
            return false;

        if(deliveryTag.name == tag)
        {
            return true;
        }


        return false;
    }
}
