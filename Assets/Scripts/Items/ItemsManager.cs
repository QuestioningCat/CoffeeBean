using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for keeping track of all Items within the running game
// when a new Item gets created it must first register itself with the ItemsManager to get its ID.
// this keeps all ID dynamic and means I do not have to deal with it XD
public class ItemsManager : MonoBehaviour
{
    private static ItemsManager _instance;

    public static ItemsManager Instance { get { return _instance; } }


    private void Awake()
    {
        // Prevent multiple instances of ItemsManager from existing at the same time.
        if(_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }



}
