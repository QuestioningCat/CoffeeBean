using CoffeeBean;
using UnityEngine;

namespace CoffeeBean.Event
{
    [CreateAssetMenu(fileName = "New Item Event", menuName = "Game Events/Item Event")]
    public class ItemEvent : BaseGameEvent<Item> { }
}