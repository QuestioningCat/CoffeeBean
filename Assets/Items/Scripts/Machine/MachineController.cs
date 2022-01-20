using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoffeeEquipmentComponentes { Portafilter, MilkJug, CoffeeBag};

public class MachineController : MonoBehaviour
{

    [Header("Attachment Points")]
    [SerializeField]
    Transform Portafilter = null;


    [Header("Hit box for interactions")]
    [SerializeField]
    BoxCollider portafilterCollider;

    
    /*
     * I am going to use a system simular to how hit boxes work in FPS games
     * Depending on where the player clicks on the machine, and with what in their hand, will determin what action the machine takes
     * 
     * if example:
     * - for the grinder, if the player clicks on the body of the machine with the portafilter
     * - then the grinder will take the portafilter from the player and add ground coffee into it.
     */

    public bool MachineAcceptedPlayerItem(Transform clickedWith)
    {
        CoffeeEquipmentComponentes type = clickedWith.GetComponentInParent<ItemManager>().GetItemSOData().type;

        switch (type)
        {
            case CoffeeEquipmentComponentes.Portafilter:
                if(Portafilter != null)
                {
                    clickedWith.position = Portafilter.position;
                    clickedWith.rotation = Portafilter.rotation;
                }
                return true;
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


}

