using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoMachineController : MonoBehaviour
{

    private List<EspressoMachine> espressoMachines;

    [Header("Event")]
    [SerializeField] private ItemEvent onItemAttached;
    [SerializeField] private ItemHitboxEvent onPlayerPickedUpItem;

    private void Awake()
    {
        espressoMachines = new List<EspressoMachine>();
    }

    public void RegisterNewEspressoMachine(EspressoMachine machine)
    {
        if(!espressoMachines.Contains(machine))
        {
            espressoMachines.Add(machine);
            //Debug.Log("Added new machine to list, Total is: " + espressoMachines.Count);
        }
    }

    public void PlayerClickedOnHitBox(ItemHitboxDataPacket dataPacket)
    {
        EspressoMachine machine = dataPacket.Hitbox.transform.GetComponentInParent<EspressoMachine>();

        if (dataPacket.Item == null && machine != null)
        {
            // The player has clicked on a grinder but is not holding anything.
            // Find out what part of the machine the player has clicked on.
            AttachmentPoint attachmentPoint = machine.GetAttachmentPoin(dataPacket.Hitbox);
            if(attachmentPoint != null)
            {
                // if there is an item attached to that part.
                if(attachmentPoint.GetAttachedItem() != null)
                {
                    ItemHitboxDataPacket dp = new ItemHitboxDataPacket(attachmentPoint.GetAttachedItem(), dataPacket.Hitbox, dataPacket.ClickedHand);
                    attachmentPoint.UpdateAttachedItem(null);
                    // raise the player pickup event.
                    onPlayerPickedUpItem.Raise(dp);
                }
            }
        }
        else
        {
            //AttachmentType type = 

            if(machine != null && espressoMachines.Contains(machine))// && dataPacket.Item.GetItemStateIndex() == 1)
            {
                AttachmentPoint attachmentPoint = machine.GetAttachmentPoin(dataPacket.Hitbox);
                switch(attachmentPoint.GetAttachmentType())
                {
                    case AttachmentType.Portafilter:
                        if(attachmentPoint != null && attachmentPoint.GetAttachedItem() == null && dataPacket.Item.GetItemStateIndex() == 1)
                        {
                            // raise the attach event
                            onItemAttached.Raise(dataPacket.Item);
                            // Update the attachmentPoints attachedItem.
                            attachmentPoint.UpdateAttachedItem(dataPacket.Item);
                            // move the Item to the attachment point
                            dataPacket.Item.transform.position = attachmentPoint.transform.position;
                            dataPacket.Item.transform.rotation = attachmentPoint.transform.rotation;
                        }
                        break;
                    case AttachmentType.MilkJug:
                        break;
                    case AttachmentType.Cup:
                        break;
                }

            }
        }
    }
}
