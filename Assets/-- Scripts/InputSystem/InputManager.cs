using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //https://forum.unity.com/threads/new-input-system-check-if-a-key-was-pressed.952571/ Could be something to look into

    //https://www.youtube.com/watch?v=LqnPeqoJRFY
    private static InputManager _instance;

    public static InputManager Instance { get { return _instance; } }

    private PlayerControls playerControls;

    private void Awake()
    {
        // Prevent multiple instances of Input Manager from existing at the same time.
        if(_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;


        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovment()
    {
        return playerControls.Player.Movment.ReadValue<Vector2>();
    }
    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumpedThisFrame()
    {
        return playerControls.Player.Jump.triggered;
    }
    public bool JumpButtonDown()
    {
        return playerControls.Player.Jump.ReadValue<float>() > 0f;
    }
    public bool JumpButtonUp()
    {
        return playerControls.Player.Jump.triggered && playerControls.Player.Jump.ReadValue<float>() == default;
    }

    public bool InteractionButtonDown()
    {
        return playerControls.Player.Interact.ReadValue<float>() > 0f;
    }    

    public bool LeftInteractThisFrame()
    {
        return playerControls.Player.LeftHandInteract.triggered;
    }

    public bool RightInteractThisFrame()
    {
        return playerControls.Player.RightHandInteract.triggered;
    }
}
