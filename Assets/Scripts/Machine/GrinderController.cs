using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderController : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private ItemEvent onItemAttached;
    [SerializeField] private ItemEvent onDropItemInHand;


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
        if (dataPacket.Item == null)
        {
            Debug.Log("DING");
        }
        else
        {
            Grinder grinder = dataPacket.Hitbox.transform.GetComponent<Grinder>();
            if(grinder != null && grinders.Contains(grinder))
            {
                AttachmentPoint attachmentPoint = GetClickedAttachmentPoit(dataPacket.Hitbox);
                if(attachmentPoint != null)
                {
                    // raise the attach event
                    onItemAttached.Raise(dataPacket.Item);
                    // move the Item to the attachment point
                    dataPacket.Item.transform.position = attachmentPoint.AttachPoint.position;
                    dataPacket.Item.transform.rotation = attachmentPoint.AttachPoint.rotation;
                    // tell the grinder to start grinding cofee
                    grinder.GrindCoffee(dataPacket.Item);
                }
            }
            else if(grinder == null)
            {

            }
        }
    }

    private AttachmentPoint GetClickedAttachmentPoit(Collider hitbox)
    {
        int grinderIndex = grinders.IndexOf(hitbox.GetComponent<Grinder>());
        if(grinderIndex < 0)
            return null;
        int hitboxIndex = grinders[grinderIndex].GetHitboxIndex(hitbox);
        AttachmentPoint attachmentPoint = grinders[grinderIndex].GetAttachmentPointFromHitboxIndex(hitboxIndex);
        return attachmentPoint;
    }
}
