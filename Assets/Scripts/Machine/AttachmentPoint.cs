using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttachmentType { Portafilter, MilkJug, Cup}

public class AttachmentPoint
{
    public AttachmentType AttachmentType { get; protected set; }
    public Transform AttachPoint { get; protected set; }
    public Item AttachedItem { get; protected set; }
    public bool InUse { get; protected set; }
    public BoxCollider Hitbox { get; protected set; }

    public AttachmentPoint(Transform point, BoxCollider hitBox, AttachmentType attachmentType)
    {
        this.AttachPoint = point;
        this.Hitbox = hitBox;
        this.AttachmentType = attachmentType;
        this.InUse = false;
        this.AttachedItem = null;
    }

    public void UpdateAttachedItem(Item item)
    {
        this.AttachedItem = item;
    }
}
