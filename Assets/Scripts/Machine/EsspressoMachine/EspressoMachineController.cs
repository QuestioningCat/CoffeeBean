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
        EspressoMachine machine = dataPacket.Hitbox.transform.GetComponent<EspressoMachine>();

        if (dataPacket.Item == null && machine != null)
        {
            // The player has clicked on a grinder but is not holding anything.
            // Find out what part of the machine the player has clicked on.
            AttachmentPoint attachmentPoint = machine.GetAttachmentPoin(dataPacket.Hitbox);
            if(attachmentPoint != null)
            {
                // if there is an item attached to that part.
                if(attachmentPoint.AttachedItem != null)
                {
                    ItemHitboxDataPacket dp = new ItemHitboxDataPacket(attachmentPoint.AttachedItem, dataPacket.Hitbox, dataPacket.ClickedHand);
                    attachmentPoint.UpdateAttachedItem(null);
                    // raise the player pickup event.
                    onPlayerPickedUpItem.Raise(dp);
                }
            }

        }
        else
        {
            if(machine != null && espressoMachines.Contains(machine) && dataPacket.Item.GetItemStateIndex() == 1)
            {
                AttachmentPoint attachmentPoint = machine.GetAttachmentPoin(dataPacket.Hitbox);
                if(attachmentPoint != null && attachmentPoint.AttachedItem == null)
                {
                    // raise the attach event
                    onItemAttached.Raise(dataPacket.Item);
                    // Update the attachmentPoints attachedItem.
                    attachmentPoint.UpdateAttachedItem(dataPacket.Item);
                    // move the Item to the attachment point
                    dataPacket.Item.transform.position = attachmentPoint.AttachPoint.position;
                    dataPacket.Item.transform.rotation = attachmentPoint.AttachPoint.rotation;
                    // tell the grinder to start grinding cofee
                    //machine.GrindCoffee(dataPacket.Item);
                }
            }
        } 
            


        //if(dataPacket.Item == null)
        //{
        //    // The player has clicked on a grinder but is not holding anything.
        //    // Find out what part of the machine the player has clicked on.
        //    AttachmentPoint attachmentPoint = GetClickedAttachmentPoit(dataPacket.Hitbox);
        //    if(attachmentPoint != null)
        //    {
        //        // if there is an item attached to that part.
        //        if(attachmentPoint.AttachedItem != null)
        //        {
        //            ItemHitboxDataPacket dp = new ItemHitboxDataPacket(attachmentPoint.AttachedItem, dataPacket.Hitbox, dataPacket.ClickedHand);
        //            attachmentPoint.UpdateAttachedItem(null);
        //            // raise the player pickup event.
        //            //onPlayerPickedUpItem.Raise(dp);
        //        }
        //    }
        //}
        //else
        //{

        //}
    }

    private AttachmentPoint GetClickedAttachmentPoiit(Collider hitbox)
    {
        int index = espressoMachines.IndexOf(hitbox.GetComponent<EspressoMachine>());
        if(index == -1)
            return null;
        //AttachmentType type = espressoMachines[index].GetAttachmentPointFromHitboxIndex(index)


        //int hitboxIndex = espressoMachines[Index].GetHitboxIndex(hitbox);
        //AttachmentPoint attachmentPoint = espressoMachines[Index].GetAttachmentPointFromHitboxIndex(hitboxIndex);
        //return attachmentPoint;
        return null;
    }




}
