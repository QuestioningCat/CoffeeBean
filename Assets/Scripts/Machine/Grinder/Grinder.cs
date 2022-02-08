using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    [Header("Template Scriptable Object")]
    [SerializeField] private Grinder_SO grinder_SO;

    [Header("HitBoxes")]
    [SerializeField] private List<BoxCollider> attachmentPointHitBoxes;
    public List<Collider> HopperHitBoxes = new List<Collider>();

    [Header("Attachment Point Positions")]
    [SerializeField] private List<Transform> attachmentPointPostions;

    [Header("Events")]
    [SerializeField] private GrinderEvent onNewGrinderCreated;
    [SerializeField] private ItemStateChangeEvent onGrindCoffeeIntoGrinder;

    [SerializeField] private List<AttachmentPoint> portafilterAttachmentPoints = new List<AttachmentPoint>();


    /// <summary>
    /// Grinds coffee from the hopper into the portafilter
    /// </summary>
    /// <param name="protafilter"></param>
    public void GrindCoffee(Item protafilter)
    {
        onGrindCoffeeIntoGrinder.Raise(new ItemDataPacket(protafilter, onGrindCoffeeIntoGrinder.NewState));
    }

    public int GetHitboxIndex(Collider hitbox)
    {
        for(int i = 0; i < portafilterAttachmentPoints.Count; i++)
        {
            if(hitbox == portafilterAttachmentPoints[i].Hitbox)
                return i;
        }
        return -1;
    }

    public AttachmentPoint GetAttachmentPointFromHitboxIndex(int hitboxIndex)
    {
        if(hitboxIndex != -1)
            return portafilterAttachmentPoints[hitboxIndex];

        return null;
    }

    private void Start()
    {
        if(attachmentPointPostions.Count == attachmentPointHitBoxes.Count)
        {
            for(int i = 0; i < attachmentPointPostions.Count; i++)
            {
                portafilterAttachmentPoints.Add(new AttachmentPoint(attachmentPointPostions[i], attachmentPointHitBoxes[i], AttachmentType.Portafilter));
                
            }
            onNewGrinderCreated.Raise(this);
        }
        else
        {
            Debug.LogError("ERROR :: HitBox and AttachmentPoints are the equal for: " + this.transform.name) ;
        }
    }
}

