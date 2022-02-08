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
    private Item itemInLeftHand = null;
    private Item itemInRightHand = null;

    [Header("Events")]
    [Tooltip("the scriptable object which will raise the event when the player tries to interact with something")]
    [SerializeField] private ItemHitboxEvent onPlayerClickedHitbox;

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

    private void PickedUpItem(Item item)
    {
        Rigidbody rb = item.transform.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
        item.GetComponent<BoxCollider>().enabled = false;

    }

    private void DropItemFromHand(Item item)
    {
        Rigidbody rb = item.transform.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.freezeRotation = false;
        item.GetComponent<BoxCollider>().enabled = true;
    }
    
    public void ItemAttachedToMachine(Item item)
    {
        int itemID = item.GetItemID();
        //Debug.Log("DINGDDD: " + item.GetHandItemIsIn().ToString());
        if(itemInLeftHand.GetComponent<Item>().GetItemID() == item.GetItemID())
        {
            if(item.GetHandItemIsIn() == Hand.LeftHand)
            {
                itemInLeftHand = null;
                item.UpdateCurrentHand(Hand.NoHands);
            }
            else if(item.GetHandItemIsIn() == Hand.RightHand)
            {
                itemInRightHand = null;
                item.UpdateCurrentHand(Hand.NoHands);

            }
        }
    }

    public void PlayerPickedUpItem(Item item, Hand hand)
    {
        if(hand == Hand.LeftHand)
        {
            itemInLeftHand = item;
            item.UpdateCurrentHand(Hand.LeftHand);
            PickedUpItem(item);
        }
        else if (hand == Hand.RightHand)
        {
            itemInRightHand = item;
            item.UpdateCurrentHand(Hand.RightHand);
            PickedUpItem(item);
        }
    }

    public void PlayerPickedUpItem(ItemHitboxDataPacket dataPacket)
    {
        Hand hand = dataPacket.ClickedHand;
        switch(hand)
        {
            case Hand.LeftHand:
                itemInLeftHand = dataPacket.Item;
                dataPacket.Item.UpdateCurrentHand(Hand.LeftHand);
                PickedUpItem(dataPacket.Item);
                break;
            case Hand.RightHand:
                itemInRightHand = dataPacket.Item;
                dataPacket.Item.UpdateCurrentHand(Hand.RightHand);
                PickedUpItem(dataPacket.Item);
                break;
            default:
                break;
        }

    }


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
            if(itemInLeftHand == null)
            {
                // we are not holding anything.
                if(Physics.Raycast(ray, out hit, pickUpDistance))
                {
                    // check to see if we are looking at something we can pick up
                    if(hit.transform.tag == "PickUp")
                    {
                        PlayerPickedUpItem(hit.transform.GetComponent<Item>(), Hand.LeftHand);
                        return;
                    }
                    else
                    {
                        onPlayerClickedHitbox.Raise(new ItemHitboxDataPacket(null, hit.collider, Hand.LeftHand));
                        return;
                    }
                }
            }
            else if(itemInLeftHand != null)
            {
                if(Physics.Raycast(ray, out hit, pickUpDistance))
                {
                    if(hit.transform.tag == "Interactable")
                    {
                        onPlayerClickedHitbox.Raise(new ItemHitboxDataPacket(itemInLeftHand.GetComponent<Item>(), hit.collider, Hand.LeftHand));
                        return;
                    }
                }


                // If we are here. Then we are looking at something and holding an item, but we cant do anything with it
                // so just drop it
                // to prevent this final action, you should return before reaching this point
                DropItemFromHand(itemInLeftHand);
                itemInLeftHand = null;
            }
        }
        else if (inputManager.RightInteractThisFrame())
        {
           
        }
    }

    private void UpdateHeldItemsPostions()
    {
        if(itemInLeftHand != null)
        {
            itemInLeftHand.transform.position = Vector3.Slerp(itemInLeftHand.transform.position, LeftHandLocationRef.position, 10f * Time.deltaTime);
            itemInLeftHand.transform.rotation = LeftHandLocationRef.rotation;
        }

        if(itemInRightHand != null)
        {
            itemInRightHand.transform.position = Vector3.Slerp(itemInRightHand.transform.position, RightHandLocationRef.position, 10f * Time.deltaTime);
            itemInRightHand.transform.rotation = RightHandLocationRef.rotation;
        }
    }


}
