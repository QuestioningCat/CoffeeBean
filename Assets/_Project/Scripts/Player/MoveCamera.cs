using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeBean.Player
{
    public class MoveCamera : MonoBehaviour
    {
        [SerializeField]
        Transform cameraPostion;

        private void Update()
        {
            transform.position = cameraPostion.position;
        }
    }
}