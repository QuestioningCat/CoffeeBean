using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttachmentType { Portafilter, MilkJug, Cup}

public class AttachmentPoint : MonoBehaviour
{
    private Item attachedItem;
    private BoxCollider hitbox;
    private AttachmentType AttachmentType;

    private void Awake()
    {
        hitbox = this.GetComponent<BoxCollider>();
        this.tag = "Interactable";
    }

    public Item GetAttachedItem()
    {
        return attachedItem;
    }

    public void UpdateAttachedItem(Item item)
    {
        this.attachedItem = item;
    }

    public AttachmentType GetAttachmentType()
    { 
        return AttachmentType; 
    }

    public BoxCollider GetHitBox()
    {
        return hitbox;
    }
}
