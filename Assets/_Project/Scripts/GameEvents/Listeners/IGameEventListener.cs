using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeBean.Event
{

    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}