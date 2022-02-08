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
    public List<Collider> HopperHitBoxes = new List<Collider>();

    [Header("Attachment Point Positions")]
    [SerializeField] private List<Transform> portafilterAttachmentPointPostions;
    [SerializeField] private List<Transform> steamWandAttachmentPointPostions;
    [SerializeField] private List<Transform> coffeeCupAttachmentPointPostions;

    [Header("Events")]
    [SerializeField] private EspressoMachineEvent onNewEspressoMachineCreated;

    [SerializeField] private List<AttachmentPoint> portafilterAttachmentPoints = new List<AttachmentPoint>();




    private void Start()
    {



        //if(attachmentPointPostions.Count == attachmentPointHitBoxes.Count)
        //{
        //    for(int i = 0; i < attachmentPointPostions.Count; i++)
        //    {
        //        portafilterAttachmentPoints.Add(new AttachmentPoint(attachmentPointPostions[i], attachmentPointHitBoxes[i]));
        //    }
        //    onNewEspressoMachineCreated.Raise(this);
        //}
        //else
        //{
        //    Debug.LogError("ERROR :: HitBox and AttachmentPoints are the equal for: " + this.transform.name);
        //}
    }
}
