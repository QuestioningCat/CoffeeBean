using System.Collections.Generic;
using UnityEngine;
using CoffeeBean.Event;

namespace CoffeeBean.Machine
{
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
                if(attachmentPoint == null)
                    continue;

                if(attachmentPoint.GetHitBox() == collider)
                    return attachmentPoint;
            }
            return null;
        }

        public bool GetTag(string tag)
        {

            if(tag == "" || tag == null)
                return false;

            foreach(Tag_SO t in espressoMachine_SO.Tags)
            {
                if(t == null)
                    continue;
                if(t.name == tag)
                {
                    return true;
                }
            }


            return false;
        }

        private void Start()
        {
            onNewEspressoMachineCreated.Raise(this);
        }
    }
}