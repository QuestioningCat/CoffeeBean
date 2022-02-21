using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer/Order")]
public class Order_SO : ScriptableObject
{
    public string OrderName;
    public List<Tag_SO> Tags;
    public Sprite Icon;
}
