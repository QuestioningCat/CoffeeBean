using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Customer", menuName = "Customer/Customer")]
public class Customer_OS : ScriptableObject
{
    public GameObject CustomerModel;

    public string Name;
}
