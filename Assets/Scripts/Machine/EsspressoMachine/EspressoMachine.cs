using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoMachine : MonoBehaviour
{
    [Header("Template Scriptable Object")]
    [SerializeField] private EspressoMachine_SO espressoMachine_SO;

    [Header("HitBoxes")]
    [SerializeField] private List<BoxCollider> portafilterAttachmentPointHitBoxes;
    [SerializeField] private List<BoxCollider> steamWandAttachmentPointHitBoxes;
    [SerializeField] private List<BoxCollider> coffeeCupAttachmentPointHitBoxes;

    [Header("Attachment Point Positions")]
    [SerializeField] private List<Transform> portafilterAttachmentPointPostions;
    [SerializeField] private List<Transform> steamWandAttachmentPointPostions;
    [SerializeField] private List<Transform> coffeeCupAttachmentPointPostions;

    [SerializeField] private List<AttachmentPoint> allAttachmentPoints = new List<AttachmentPoint>();


    [Header("Events")]
    [SerializeField] private EspressoMachineEvent onNewEspressoMachineCreated;


    public int GetHitboxIndex(Collider hitbox)
    {
        //for(int i = 0; i < portafilterAttachmentPoints.Count; i++)
        //{
        //    if(hitbox == portafilterAttachmentPoints[i].Hitbox)
        //        return i;
        //}
        return -1;
    }

    public AttachmentPoint GetAttachmentPoin(Collider collider)
    {
        foreach(AttachmentPoint attachmentPoint in allAttachmentPoints)
        {
            if(attachmentPoint.GetHitBox() == collider)
                return attachmentPoint;
        }
        return null;
    }





    private void Start()
    {

        if( portafilterAttachmentPointPostions.Count == portafilterAttachmentPointHitBoxes.Count && portafilterAttachmentPointPostions.Count > 0)
        {
            for(int i = 0; i < portafilterAttachmentPointPostions.Count; i++)
            {
                //allAttachmentPoints.Add(new AttachmentPoint(portafilterAttachmentPointPostions[i], portafilterAttachmentPointHitBoxes[i], AttachmentType.Portafilter));
            }
        }
        else
        {
            Debug.LogError("ERROR :: HitBox and AttachmentPoints are the equal for: " + this.transform.name);
        }

        if( steamWandAttachmentPointPostions.Count == steamWandAttachmentPointHitBoxes.Count && steamWandAttachmentPointPostions.Count > 0 )
        {
            for(int i = 0; i < steamWandAttachmentPointPostions.Count; i++)
            {
                //allAttachmentPoints.Add(new AttachmentPoint(steamWandAttachmentPointPostions[i], steamWandAttachmentPointHitBoxes[i], AttachmentType.MilkJug));
            }
        }
        else
        {
            //Debug.LogError("ERROR :: HitBox and AttachmentPoints are the equal for: " + this.transform.name);
        }

        if( coffeeCupAttachmentPointPostions.Count == coffeeCupAttachmentPointHitBoxes.Count && coffeeCupAttachmentPointPostions.Count > 0 )
        {
            for(int i = 0; i < portafilterAttachmentPointPostions.Count; i++)
            {
                //allAttachmentPoints.Add(new AttachmentPoint(coffeeCupAttachmentPointPostions[i], coffeeCupAttachmentPointHitBoxes[i], AttachmentType.Cup));
            }
        }
        else
        {
            //Debug.LogError("ERROR :: HitBox and AttachmentPoints are the equal for: " + this.transform.name);
        }

        onNewEspressoMachineCreated.Raise(this);

    }
}