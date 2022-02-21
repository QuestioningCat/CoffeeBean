using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Will hold all data related to the customers order.
/// Eg: type of drink, whene they ordered it, (rest to add later ->) Expected quality
/// </summary>
public class CustomerOrder
{
    // if all tags on the given drink mach, then the order is correct
    public Order_SO CoffeeOrder { get; protected set; }

    // if player is taking to look to make the order then the guest will get angry, or even leave.
    public float OrderedTime;

    public CustomerOrder(Order_SO order, float orderedTime)
    {
        this.CoffeeOrder = order;
        this.OrderedTime = orderedTime;
    }

    public bool isOrderCorrect(Order_SO givenOrder)
    {
        if(givenOrder != CoffeeOrder)
        {
            Debug.Log("WRONG order");
            return false; // order cant be correct.
        }
        return true;
    }

}
