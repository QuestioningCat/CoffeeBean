using CoffeeBean.Event;
using System.Collections.Generic;
using UnityEngine;


namespace CoffeeBean
{
    public class InteractionObjectController : MonoBehaviour
    {
        [Header("Event")]
        [SerializeField] private CraftingEvent onNewItemCrafted;

        private List<InteractionObject> interactionObjects;

        private void Awake()
        {
            interactionObjects = new List<InteractionObject>();
        }

        /// <summary>
        /// Registers the given Espresso Machine to the list of all Espresso Machines
        /// </summary>
        /// <param name="machine"></param>
        public void RegisterNewInteractionObject(InteractionObject interactionObject)
        {
            if(this.interactionObjects.Contains(interactionObject))
            {
                return;
            }

            this.interactionObjects.Add(interactionObject);
        }


        /// <summary>
        /// Gets called when the player has clicked on a hitbox, Collider
        /// </summary>
        /// <param name="dataPacket"></param>
        public void PlayerClickedOnHitBox(ItemHitboxDataPacket dataPacket)
        {


        }
    }
}