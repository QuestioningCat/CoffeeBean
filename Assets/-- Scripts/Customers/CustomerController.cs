using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// use the controll what the customer is doing based on their current state.
/// for example. if the customers is WaitingToOrder, then they are in line to place an order.
/// Only one customer can be in the Ordering state at a time.
/// Idel is a default state where they will just stand or wonder around the shop.
/// </summary>
public enum CustomersCurrentState { Idel, WaitingToOrder, Ordering, Leaving, Drinking}


public class CustomerController : MonoBehaviour
{
    // List of all customers in the bulding.
    private List<Customer> customers;

    //TEMP::
    /*
     * For now we will just have the customer controller generate an order.
     * later donw the line we can swap this out for a dedicated script to controll
     * more complicated ordering logic. For example, a systme that would allow the
     * player to alter the menue dynamicaly. Or a way of giving a byus to a spacifc menue item,
     * eg. a limited item offer.
     */

    // this will hold all possible coffee tags that are on the current menue.
    public List<Order_SO> CoffeeMenue;

    private void Awake()
    {
        customers = new List<Customer>();
    }



    public void RegisterNewCustomer(CustomerDataPacket dataPacket)
    {
        if (dataPacket.Customer.order == null)
        {
            GenerateNewOrder(dataPacket.Customer);
        }
    }

    private void GenerateNewOrder(Customer customer)
    {
        int selection = Random.Range(0, CoffeeMenue.Count);

        /* At the moment the list is only of length 1. this is due to use not using any modifiers (like syrups, or chocolet powder on top)
         * and we are not currently accounting for half full cups, this would happen when you used the wrong sized milk jug to make a latte.
         * Or if you have a single espresso shot instead of a double.
         */

        Order_SO orderSelection = CoffeeMenue[Mathf.RoundToInt(Random.Range(0,CoffeeMenue.Count))];
        CustomerOrder order = new CustomerOrder(orderSelection, Time.time);
        customer.UpdateCustomersOrder(order);
    }
}
