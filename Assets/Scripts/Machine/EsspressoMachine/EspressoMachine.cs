using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoMachine : MonoBehaviour
{
    [Header("Template Scriptable Object")]
    [SerializeField] private EspressoMachine_SO espressoMachine_SO;

    [SerializeField] private List<AttachmentPoint> allAttachmentPoints = new List<AttachmentPoint>();

    [Header("Events")]
    [SerializeField] private EspressoMachineEvent onNewEspressoMachineCreated;

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
        onNewEspressoMachineCreated.Raise(this);
    }
}