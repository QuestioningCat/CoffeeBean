using CoffeeBean.Event;
using System.Collections.Generic;
using UnityEngine;
using CoffeeBean.Tag;

namespace CoffeeBean.Machine
{

    public class Grinder : MonoBehaviour
    {
        [Header("Template Scriptable Object")]
        [SerializeField] private Grinder_SO grinder_SO;

        [Header("Events")]
        [SerializeField] private GrinderEvent onNewGrinderCreated;
        [SerializeField] private ItemStateChangeEvent onGrindCoffeeIntoGrinder;

        [Header("Attachment Points")]
        // Possible improvment would be to generate this list at runtime to further reduce complexity for the designers.
        [SerializeField] private List<AttachmentPoint> attachmentPoints = new List<AttachmentPoint>();


        /// <summary>
        /// Grinds coffee from the hopper into the portafilter
        /// </summary>
        /// <param name="portafilter"></param>
        public void GrindCoffee(Item portafilter)
        {
            if(portafilter.GetItemStateIndex() != 0)
                return;
            onGrindCoffeeIntoGrinder.Raise(new ItemDataPacket(portafilter, onGrindCoffeeIntoGrinder.NewState));
        }
        public bool GetTag(string tag)
        {

            if(tag == "" || tag == null)
                return false;

            foreach(Tag_SO t in grinder_SO.Tags)
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

        /// <summary>
        /// Returns the attachment point for the given collider
        /// </summary>
        /// <param name="collider"></param>
        /// <returns></returns>
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
            // Register the grind with it's controller
            onNewGrinderCreated.Raise(this);
        }
    }

}