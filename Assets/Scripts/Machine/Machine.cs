using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Holds all backend data for all machines in the world
/// </summary>
public class Machine : MonoBehaviour
{
    [Header("Scriptable Object")]
    [Tooltip("The scriptable object template")]
    [SerializeField] Machine_SO machine_SO;

    [Header("Events")]
    [SerializeField] private MachineEvent onMachineCreated;
    [SerializeField] private ItemStateChangeEvent onCoffeeGround;


    private int ID = -1;

    private bool registered = false;

    public void UpdateMachineID(int newID) { ID = newID; }
    public int GetMachineID() { return ID; }

    public Machine_SO GetMachine_OS() { return machine_SO; }

    [Tooltip("These Indexes need to the be same as the Atachment Points")]
    [Header("Interaction Hit Boxes")]
    public BoxCollider CoffeeBeansHopperHitBoxes;

    public List<BoxCollider> GrinderHitBoxes = new List<BoxCollider>();
    public List<BoxCollider> EsspresssoMachineHitBoxes = new List<BoxCollider>();
    public List<BoxCollider> SteamWandHitBoxes = new List<BoxCollider>();

    [Tooltip("These Indexes need to the be same as the hitboxes")]
    [Header("Interaction Positions")]
    public List<Transform> GrinderAtachmentPoints = new List<Transform>();
    public List<Transform> EsspresssoMachineAtachmentPoints = new List<Transform>();
    public List<Transform> SteamWandAtachmentPoints = new List<Transform>();

    private List<AtachmentPoint> allAttachmentPoints = new List<AtachmentPoint>();
    /// <summary>
    /// Gets raised when Player has click on a Grinder hit box
    /// </summary>
    public void GrindCoffeeIntoPortafilter(MachineItemDataPacket machineItemDataPacket)
    {
        Item clickedWith = machineItemDataPacket.Item;
        Machine clickedOn = machineItemDataPacket.Machine;
        RaycastHit hitCollider = machineItemDataPacket.Hit;

        int index = GetAttachmentPointIndex(hitCollider.collider);

        if (index > -1 && allAttachmentPoints[index].currentlyAttached == null)
        {
            clickedWith.transform.position = GrinderAtachmentPoints[index].position;
            clickedWith.transform.rotation = GrinderAtachmentPoints[index].rotation;
            allAttachmentPoints[index].AttachItem(clickedWith);

            // lastly raise the event to update the portafilters model state
            ItemDataPacket itemData = new ItemDataPacket(clickedWith, onCoffeeGround.NewState);
            onCoffeeGround.Raise(itemData);
        }
    }

    public bool IsSpaceAvalable(Collider hitBox)
    {
        int index = GetAttachmentPointIndex(hitBox);

        if(allAttachmentPoints[index].currentlyAttached != null)
        {
            return false;
        }
        return true;
    }
    public Transform PlayerPickedUpItem(Collider hitbox)
    {
        int index = GetAttachmentPointIndex(hitbox);
        Transform currentlyAttached = allAttachmentPoints[index].currentlyAttached.transform;
        allAttachmentPoints[index].DetachCurrentlyAttachedItem();
        return currentlyAttached;
    }

    private int GetAttachmentPointIndex(Collider collider)
    {
        // place portafilter into coffee machine
        foreach(BoxCollider box in GrinderHitBoxes)
        {
            if(box == collider)
            {
                return GrinderHitBoxes.IndexOf(box);
            }
        }
        return -1;
    }

    private void Awake()
    {
        /* Remember if you want to find the index of this list. You will need to add the total count to the index before adding the index you want
         * for example. If you want to look for just a grinder then that is find because you do not have to add anything.
         * But if you want to find a steam wand, then you will first need to add the total of both the grinder and the esspresso points
         */
        for(int i = 0; i < GrinderAtachmentPoints.Count; i++)
        {
            AtachmentPoint attachmentPoint = new AtachmentPoint(GrinderAtachmentPoints[i], GrinderHitBoxes[i]);
            allAttachmentPoints.Add(attachmentPoint);
        }
        for(int i = 0; i < EsspresssoMachineAtachmentPoints.Count; i++)
        {
            AtachmentPoint attachmentPoint = new AtachmentPoint(EsspresssoMachineAtachmentPoints[i], EsspresssoMachineHitBoxes[i]);
            allAttachmentPoints.Add(attachmentPoint);
        }
        for(int i = 0; i < SteamWandAtachmentPoints.Count; i++)
        {
            AtachmentPoint attachmentPoint = new AtachmentPoint(SteamWandAtachmentPoints[i], SteamWandHitBoxes[i]);
            allAttachmentPoints.Add(attachmentPoint);
        }
    }
    private void Start()
    {
        onMachineCreated.Raise(this);
        registered = true;
    }
    private void OnEnable()
    {
        if(ID < 0 && registered)
        {
            onMachineCreated.Raise(this);
        }
    }
}

class AtachmentPoint
{
    public BoxCollider HitBox { get; protected set; }
    public Transform AttachmentPoint { get; protected set; }
    public Item currentlyAttached { get; protected set; }
    /// <summary>
    /// Will keep track of a spacific part of the machine that the player can interact with
    /// Can tell us if a attachment point is in use or not
    /// Can tell us where the attachment point is.
    /// Can tell us where there attachment points BoxColider is.
    /// </summary>
    /// <param name="attachmentPoint"></param>
    /// <param name="hitBox"></param>
    public AtachmentPoint(Transform attachmentPoint, BoxCollider hitBox)
    {
        this.AttachmentPoint = attachmentPoint;
        this.HitBox = hitBox;
        this.currentlyAttached = null;
    }

    public void AttachItem (Item item) { currentlyAttached = item; }

    public void DetachCurrentlyAttachedItem()
    {
        currentlyAttached = null;
    }
}
