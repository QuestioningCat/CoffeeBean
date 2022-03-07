using CoffeeBean.Machine;
using UnityEngine;

namespace CoffeeBean.Event
{
    [CreateAssetMenu(fileName = "New Grinder Event", menuName = "Game Events/Machines/Grinder Event")]
    public class GrinderEvent : BaseGameEvent<Grinder> { }
}