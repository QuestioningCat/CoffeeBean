using UnityEngine;
using CoffeeBean.Input;

namespace CoffeeBean.Player
{

    public class PlayerMovment : MonoBehaviour
    {

        private InputManager inputManager;

        Vector3 moveDir;
        Vector3 slopeMoveDir;

        Rigidbody rb;

        [SerializeField]
        Transform orientation;

        [Header("Movement")]
        [SerializeField]
        float moveSpeed = 6f;

        float airMultiplier = 0.4f;
        float movementMultiplier = 10f;

        float horizontalMovment;
        float verticalMovement;

        float jumpForce = 15f;
        float fallMult = 5f;

        float playerHeight = 2f;

        float groundDrag = 6f;
        float airDrag = 4f;

        [Header("Ground Detection")]
        [SerializeField]
        LayerMask groundMask;
        float groundDistance = 0.4f;
        bool isGrounded;

        RaycastHit slopeHit;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            inputManager = InputManager.Instance;
            rb.freezeRotation = true;
        }

        private void Update()
        {
            isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

            MyInput();
            ControlDrag();

            if(inputManager.PlayerJumpedThisFrame() && isGrounded)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }

            if(rb.velocity.y < 0)
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMult - 1) * Time.deltaTime;


            slopeMoveDir = Vector3.ProjectOnPlane(moveDir, slopeHit.normal);
        }
        private bool OnSlope()
        {
            if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
            {
                if(slopeHit.normal != Vector3.up)
                    return true;
            }
            return false;
        }
        void ControlDrag()
        {
            if(isGrounded)
                rb.drag = groundDrag;
            else if(!isGrounded)
                rb.drag = airDrag;
        }
        void MyInput()
        {
            horizontalMovment = inputManager.GetPlayerMovment().x;
            verticalMovement = inputManager.GetPlayerMovment().y;

            moveDir = orientation.forward * verticalMovement + orientation.right * horizontalMovment;
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }
        void MovePlayer()
        {
            if(isGrounded && !OnSlope())
                rb.AddForce(moveDir.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            else if(isGrounded && OnSlope())
                rb.AddForce(slopeMoveDir.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            else if(!isGrounded)
                rb.AddForce(moveDir.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);

        }
    }
}