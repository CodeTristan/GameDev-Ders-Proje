using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;
    Rigidbody rb;
    [SerializeField] Transform orientation;

    [SerializeField] float speed = 7;

    void Start()
    {
        //accessing the input actions
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");

        //freeze rotation so player doesnt fall
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }


    void Update()
    {
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
        Vector3 moveDirection = (forward * direction.y + right * direction.x).normalized;

        // Apply movement
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
