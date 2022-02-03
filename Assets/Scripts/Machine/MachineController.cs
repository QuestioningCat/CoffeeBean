using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoffeeEquipmentComponentes { Portafilter, MilkJug, CoffeeBag};

public class MachineController : MonoBehaviour
{

    [Header("Attachment Points")]
    [SerializeField]
    Transform portafilterAtachmentPoint = null;


    [Header("Hit box for interactions")]
    [SerializeField]
    BoxCollider portafilterCollider = null;

    //[Header("Events")]
    //[Tooltip("the scriptable object which will raise the event when a portafilter is attached")]
    //public GameEvent portafilterAttached;

    public VoidEvent OnTestVoidEvent;

    private Transform portafilter = null;

    
    /*
     * I am going to use a system simular to how hit boxes work in FPS games
     * Depending on where the player clicks on the machine, and with what in their hand, will determin what action the machine takes
     * 
     * if example:
     * - for the grinder, if the player clicks on the body of the machine with the portafilter
     * - then the grinder will take the portafilter from the player and add ground coffee into it.
     */

    public bool MachineAcceptedPlayerItem(Transform clickedWith, Collider hitCollider)
    {
        CoffeeEquipmentComponentes type = clickedWith.GetComponentInParent<ItemManager>().GetItemSOData().type;
        switch (type)
        {
            case CoffeeEquipmentComponentes.Portafilter:
                if(portafilterAtachmentPoint != null && hitCollider == portafilterCollider && portafilter == null)
                {

                    portafilter = clickedWith;
                    clickedWith.position = portafilterAtachmentPoint.position;
                    clickedWith.rotation = portafilterAtachmentPoint.rotation;

                    if(portafilter != null)
                    {
                        //if(GameEventHub.EquipmentAttached.ItemThatWasAttached != null)
                        //    portafilterAttached.Raise();
                        //else
                            Debug.Log("DING");

                        OnTestVoidEvent.Raise();
                    }
                    return true;
                }
                break;
            case CoffeeEquipmentComponentes.MilkJug:
                break;
            case CoffeeEquipmentComponentes.CoffeeBag:
                break;
            default:
                Debug.LogError("ERROR:: machine reseaved unrecognised item type");
                return false;
        }


        return false;
    }

    public Transform PlayerPickedUpItem(Collider hitCollider)
    {
        if(hitCollider == portafilterCollider)
        {
            Transform tempT = portafilter;
            portafilter = null;
            return tempT;
        }

        // there is nothing to pick up
        return null;
    }

}
