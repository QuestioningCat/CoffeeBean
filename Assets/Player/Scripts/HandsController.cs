using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    private InputManager inputManager;

    [Header("Hands")]
    [SerializeField]
    Transform LeftHandLocationRef;
    [SerializeField]
    Transform RightHandLocationRef;

    [Header("Camera")]
    [SerializeField]
    Transform cameraHolder;

    // What the hands are holding
    private Transform TransformInLeftHand = null;
    private Transform TransformInRightHnad = null;

    // distance the player can be away from something to pick it up
    private float pickUpDistance = 2.5f; 

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
        UpdateHeldItemsPostions();

        // Just going to make it ugly for now, will clean up later :)
        if (inputManager.LeftInteractThisFrame())
        {
            RaycastHit hit;
            Ray ray = new Ray(cameraHolder.position, cameraHolder.forward);
            if(TransformInLeftHand == null)
            {
                // we are not holding anything.
                if(Physics.Raycast(ray, out hit, pickUpDistance))
                {
                    MachineController machineController = hit.transform.GetComponent<MachineController>();
                    if(machineController != null)
                    {
                        // the player has clicked on a part of the machine
                        // check to see if there is anything in that part of the machine that can picked up
                        Transform tempItem = machineController.PlayerPickedUpItem(hit.collider);
                        if (tempItem != null)
                        {
                            // the player picked up the Item from the machine
                            PickUpItemInHand(tempItem, LeftHandLocationRef);
                            TransformInLeftHand = tempItem;
                        }
                    }
                    else if(hit.transform.tag == "PickUp") // check to see if we are looking at something we can pick up
                    {
                        PickUpItemInHand(hit.transform, LeftHandLocationRef);
                        TransformInLeftHand = hit.transform;
                    }
                }
            }
            else if(TransformInLeftHand != null)
            {
                if (Physics.Raycast(ray, out hit, pickUpDistance))
                {
                    MachineController machineController = hit.transform.GetComponent<MachineController>();
                    if (machineController != null)
                    {
                        // the player is holding an item and has clicked on an machine which can accept items the player can pick up
                        // check to see what hit box the player has clicked on.
                        if(machineController.MachineAcceptedPlayerItem(TransformInLeftHand, hit.collider))
                        {
                            TransformInLeftHand = null;
                        }
                        return;
                    }
                }

                // If we are here. Then we are looking at something and holding an item, but we cant do anything with it
                // so just drop it
                // to prevent this final action, you should return before reaching this point
                // We are holding something and need to drop it
                DropItemFromHand(TransformInLeftHand, LeftHandLocationRef);
                TransformInLeftHand = null;
            }
        }
        else if (inputManager.RightInteractThisFrame())
        {
            if(TransformInRightHnad == null)
            {
                // we are not holding anything.
                RaycastHit hit;
                if(Physics.Raycast(cameraHolder.position, cameraHolder.forward, out hit, 2.5f))
                {
                    // check to see if we are looking at something we can pick up
                    if(hit.transform.tag == "PickUp")
                    {
                        PickUpItemInHand(hit.transform, RightHandLocationRef);
                        TransformInRightHnad = hit.transform;
                    }
                }
            }
            else if(TransformInRightHnad != null)
            {
                // We are holding something and need to drop it
                DropItemFromHand(TransformInRightHnad, RightHandLocationRef);
                TransformInRightHnad = null;
            }
        }
    }

    private void UpdateHeldItemsPostions()
    {
        if(TransformInLeftHand != null)
        {
            TransformInLeftHand.position = Vector3.Slerp(TransformInLeftHand.position, LeftHandLocationRef.position, 10f * Time.deltaTime);
            TransformInLeftHand.rotation = LeftHandLocationRef.rotation;
        }

        if(TransformInRightHnad != null)
        {
            TransformInRightHnad.position = Vector3.Slerp(TransformInRightHnad.position, RightHandLocationRef.position, 10f * Time.deltaTime);
            TransformInRightHnad.rotation = RightHandLocationRef.rotation;
        }
    }

    private void PickUpItemInHand(Transform item, Transform handLocationReference)
    {
        Rigidbody rb = item.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
        item.GetComponent<BoxCollider>().enabled = false;

    }
    private void DropItemFromHand(Transform item, Transform hand)
    {
        Rigidbody rb = item.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.freezeRotation = false;
        item.GetComponent<BoxCollider>().enabled = true;
    }
}
