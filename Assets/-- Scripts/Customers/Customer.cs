using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerState CurrentState { get; protected set; }

    [SerializeField] private SpeechBubble speechBubble;

    public CustomerOrder order { get; protected set; }

    [Header("Events")]
    [SerializeField] private CustomerEvent onCustomerCreated;

    private void Start()
    {
        onCustomerCreated.Raise(new CustomerDataPacket(this));
    }

    public void UpdateCustomersOrder(CustomerOrder order)
    {
        if(order == null)
            return;
        this.order = order;

        speechBubble.transform.gameObject.SetActive(true);
        speechBubble.UpdateSpeechBouble(this.order.CoffeeOrder.Icon, this.order.CoffeeOrder.OrderName, GameObject.FindGameObjectWithTag("Player")?.transform);
    }

    public void UpdateCustomerCurrentState(CustomerState state)
    {
        CurrentState = state;
    }
    
}
