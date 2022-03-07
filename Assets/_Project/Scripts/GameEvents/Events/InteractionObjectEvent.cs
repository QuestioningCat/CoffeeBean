using CoffeeBean;
using UnityEngine;

namespace CoffeeBean.Event
{
    [CreateAssetMenu(fileName = "New Interaction Object Event", menuName = "Game Events/Interaction Object/Interaction Object Event")]
    public class InteractionObjectEvent : BaseGameEvent<InteractionObject> { }
}