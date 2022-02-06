using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Will hold information for an interaction event in the game.
 * This Event could be the player wanting to place the portafilter into the grinder,
 * or it could be them wanting to fill the grind with coffee bean.
 * As such, will need to hold:
 *  -   The GameObject that was clicked
 *  -   What it was clicked with, EG. Portafilter
 *  -   What hitbox was clicked on, neede to know whcih attachment point to use.
 */
public class MachineItemDataPacket
{
    /// <summary>
    /// The Item the player click on when the event was raised.
    /// </summary>
    public Machine Machine { get; protected set; }
    /// <summary>
    /// The Item the player was holding when the event was raised.
    /// </summary>
    public Item Item { get; protected set; }
    /// <summary>
    /// The raycast hit return of the hit box that was clicked on.
    /// </summary>
    public RaycastHit Hit { get; protected set; }


    public MachineItemDataPacket(Machine Machine, Item Item, RaycastHit hit)
    {
        this.Machine = Machine;
        this.Item = Item;
        this.Hit = hit;
    }
}
