using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 7f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float airMultiplier;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool isGrounded;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody rb;
    [SerializeField] Transform orientation;



    void Start()
    {
        //accessing the input actions
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        jumpAction = playerInput.actions.FindAction("Jump");

        //freeze rotation so player doesnt fall
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }


    void Update()
    {
        //checking if the player is on ground and jump button is pressed

        if (isGrounded && jumpAction.triggered)
        {
            PlayerJump();
        }

        //handle drag
        if (isGrounded)
        {//if player is touching the ground, apply drag
            rb.drag = groundDrag;
        }
        else
        {//if player is on the air, don't apply drag
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        //checking if player is on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        //call the method for player movement
        MovePlayer();

        
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();


        // Get the forward and right directions based on the Orientation object
        Vector3 forward = orientation.forward;
        Vector3 right = orientation.right;

        // Ensure movement stays on the horizontal plane (ignore vertical rotation)
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Calculate the movement direction relative to the Orientation
        Vector3 moveDirection = (forward * direction.y + right * direction.x).normalized * speed;

        // Apply movement
        if (isGrounded)
        {
            rb.velocity = new Vector3(moveDirection.x,rb.velocity.y, moveDirection.z) ;
        }
        else if (!isGrounded)
        {//if not grounded change the speed with airMultiplier
            moveDirection *= airMultiplier;
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }
    }

    void PlayerJump()
    {
        Debug.Log("JUMP");
        //reset y velocity when jumping
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
