using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    [Header("Template Scriptable Object")]
    [SerializeField] private Grinder_SO grinder_SO;

    [Header("HitBoxes")]
    public List<Collider> HopperHitBoxes = new List<Collider>();

    [Header("Events")]
    [SerializeField] private GrinderEvent onNewGrinderCreated;
    [SerializeField] private ItemStateChangeEvent onGrindCoffeeIntoGrinder;

    [Header("Attachment Points")]
    [SerializeField] private List<AttachmentPoint> attachmentPoints = new List<AttachmentPoint>();


    /// <summary>
    /// Grinds coffee from the hopper into the portafilter
    /// </summary>
    /// <param name="protafilter"></param>
    public void GrindCoffee(Item protafilter)
    {
        onGrindCoffeeIntoGrinder.Raise(new ItemDataPacket(protafilter, onGrindCoffeeIntoGrinder.NewState));
    }

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
        onNewGrinderCreated.Raise(this);
    }
}

