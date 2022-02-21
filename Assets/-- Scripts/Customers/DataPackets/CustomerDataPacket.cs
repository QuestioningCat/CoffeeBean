using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDataPacket
{
    public Customer Customer { get; protected set; }

    public CustomerDataPacket(Customer customer)
    {
        this.Customer = customer;
    }
}
