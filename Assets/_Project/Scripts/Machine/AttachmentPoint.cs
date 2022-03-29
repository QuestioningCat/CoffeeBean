using UnityEngine;

namespace CoffeeBean.Machine
{

    //public enum AttachmentType { Portafilter, MilkJug, Cup}

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
        [SerializeField] private Tag_SO attachmentType;


        /// <summary>
        /// If this attachment point is paired with another attachment point.
        /// for example the coffee cup attachmnet point will be paired with the espresso machine group head it is under.
        /// </summary>
        [SerializeField] private AttachmentPoint attachmentPointPair;

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

        public Tag_SO GetAttachmentType()
        {
            return attachmentType;
        }

        public AttachmentPoint GetAttachmentPointPair()
        {
            return attachmentPointPair ? attachmentPointPair : null;
        }

        public BoxCollider GetHitBox()
        {
            return hitbox;
        }
    }

}