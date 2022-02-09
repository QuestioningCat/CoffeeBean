using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttachmentType { Portafilter, MilkJug, Cup}

/// <summary>
/// Attachment Points are created in the Unity Edditor.
/// These are used to create points to attach Items to depending on the Attachment Type set on the object
/// ** IMPORTANT ** An Attachment point REQUIERS BoxCollider.
///     - this BoxCollider is used as the click point for the player.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class AttachmentPoint : MonoBehaviour
{
    /// <summary>
    /// The Item currently attached to this attachment point
    /// </summary>
    private Item attachedItem;
    /// <summary>
    /// The trigger collider for this attachment point.
    /// </summary>
    private BoxCollider hitbox;
    /// <summary>
    /// The Type of Item this attachment point will accept.
    /// </summary>
    [SerializeField] private AttachmentType AttachmentType;

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
