using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    private InputManager inputManager;

    [Header("Hands")]
    [SerializeField]
    Transform LeftHandTransform;
    [SerializeField]
    Transform RightHandTransform;

    [Header("Camera")]
    [SerializeField]
    Transform cameraHolder;

    // What the hands are holding
    private Transform LeftHandHolding = null;
    private Transform RightHandHolding = null;

    // if the on the of the hand interact buttons is pressed
    // and we are looking at an item that we can pick up
    // then pick up that item.
    // if we are not looking at an item that we can pick up
    // and we are holding something, then drop that item.
    // if we are holding an item 
    // and looking at an item that we can pick up
    // then swap what we have in out hand with the item we are looking at.

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void Update()
    {

        // Just going to make it ugly for now, will clean up later :)
        if (inputManager.LeftInteractThisFrame() && LeftHandHolding == null)
        {
            // we are not holding anything.
            RaycastHit hit;
            if (Physics.Raycast(cameraHolder.position, cameraHolder.forward, out hit, 3f))
            {
                // check to see if we are looking at something we can pick up
                // TODO::
                /*
                 * Make a scriptable object that holds all data relivent to an item that can be picked up.
                 * this SO will hold information regardning what is in the container and how hot it is.
                 * My plan is to use this for as many different items as possible
                 * for example, the Portafilter will hold coffee, and the game will need to know how hot the filter is to determin
                 * how final quality of the coffee based on the temprature change during the extraction prossess
                 * this SO can also be used for jugs for milk. The jug will hold information about how much milk is in it and how much air is in it
                 * This can be used later to determin what type of coffee has been made.
                 * The coffee can also be represented with this SO, as it will hold the price and the quality of the coffee.
                 */
            }
        }


    }
}
