using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this scrip will handle all interactions between any item that is in the players hand (or in the game?) and the machine
 * that the items is being used on.
 * This scrip will then rais the corsponding events to let all involved parties know.
 */
public class ItemMachineInteractionHandler : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private MachineItemEvent onPortafilterAttachedToGrinder;
    [SerializeField] private MachineItemEvent onPortafilterAttachedToEsspressoMachine;
    [SerializeField] private ItemEvent onItemAttached;


    // Every time a new interaction occurs bettween a machine and an item, this method will be raise.
    public void NewMachineItemInteractionEventRaised(MachineItemDataPacket machineItemDataPacket)
    {
        Item clickedWith = machineItemDataPacket.Item;
        Machine clickedOn = machineItemDataPacket.Machine;
        RaycastHit hitCollider = machineItemDataPacket.Hit;

        CoffeeEquipmentComponentes type = clickedWith.GetItemSOData().type;
        switch(type)
        {
            case CoffeeEquipmentComponentes.Portafilter:
                int stateIndex = clickedWith.GetItemStateIndex();
                switch(stateIndex)
                {
                    case 0:
                        // Only Grinders will accept empty portafilters
                        if(clickedOn.IsSpaceAvalable(hitCollider.collider))
                        {
                            //ItemDataPacket itemData = new ItemDataPacket(clickedWith, onPortafilterAttachedToGrinder.NewState);
                            MachineItemDataPacket machineItem_DataPacket = new MachineItemDataPacket(clickedOn, clickedWith, hitCollider);
                            onPortafilterAttachedToGrinder.Raise(machineItem_DataPacket);
                            onItemAttached.Raise(clickedWith);
                        }
                        break;
                    case 1:
                        // Only Esspresso machines will accept full but not used portafilters
                        if (clickedOn.IsSpaceAvalable(hitCollider.collider))
                        {
                            MachineItemDataPacket machineItem_DataPacket = new MachineItemDataPacket(clickedOn, clickedWith, hitCollider);
                            onPortafilterAttachedToEsspressoMachine.Raise(machineItem_DataPacket);
                            onItemAttached.Raise(clickedWith);
                        }
                        break;
                    default:
                        break;
                }


                break;
            case CoffeeEquipmentComponentes.MilkJug:
                break;
            case CoffeeEquipmentComponentes.CoffeeBag:
                break;
            default:
                Debug.LogError("ERROR:: machine reseaved unrecognised item type");
                break;
        }
    }
}
