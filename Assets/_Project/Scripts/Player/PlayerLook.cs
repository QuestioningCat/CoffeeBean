using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoffeeBean.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField]
        private float sensX;
        [SerializeField]
        private float sensY;

        [SerializeField]
        Transform cam;
        [SerializeField]
        Transform orientation;

        private InputManager inputManager;

        float mouseX;
        float mouseY;

        float multiplier = 0.01f;

        float xRotation;
        float yRotation;

        private void Start()
        {
            inputManager = InputManager.Instance;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            MyInput();

            cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        private void MyInput()
        {
            mouseX = inputManager.GetMouseDelta().x;
            mouseY = inputManager.GetMouseDelta().y;

            yRotation += mouseX * sensX * multiplier;
            xRotation -= mouseY * sensY * multiplier;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        }
    }
}