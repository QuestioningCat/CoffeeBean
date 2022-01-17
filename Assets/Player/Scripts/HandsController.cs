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
        if (LeftHandHolding != null)
        {
            LeftHandHolding.position = Vector3.Lerp(LeftHandHolding.position, LeftHandTransform.position, 10f * Time.deltaTime);
            LeftHandHolding.rotation = LeftHandTransform.rotation;
        }


        // Just going to make it ugly for now, will clean up later :)
            if (inputManager.LeftInteractThisFrame() && LeftHandHolding == null)
        {
            // we are not holding anything.
            RaycastHit hit;
            if (Physics.Raycast(cameraHolder.position, cameraHolder.forward, out hit, 2.5f))
            {
                // check to see if we are looking at something we can pick up
                if(hit.transform.tag == "PickUp")
                {
                    PickUpItemInHand(hit.transform, LeftHandTransform);
                    LeftHandHolding = hit.transform;
                }
            }
        }


    }

    private void PickUpItemInHand(Transform item, Transform hand)
    {
        Rigidbody rb = item.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
        item.GetComponent<BoxCollider>().enabled = false;

    }
}
