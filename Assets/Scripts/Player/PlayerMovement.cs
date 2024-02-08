using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody myRigidbody;
    Animator myAnimator;

    [SerializeField]float moveSpeed = 5;

    float jumpForce = 200;
    float jumpCoolTime = 1;
    float timeSinceJump = 0;

    InputManager inputManager;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        MoveInDirectionOfCamera();
        Jump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        float yNormal = collision.contacts[0].normal.y;

        if(yNormal > 0.7f) 
        {
            myAnimator.SetTrigger("Landed");
        }
    }

    private void MoveInDirectionOfCamera()
    {
        // Get the input from the InputManager, I separate the inputs to make it easier to understand
        float horizontalInput = InputManager.moveInput.x;
        float verticalInput = InputManager.moveInput.y;

        // Calculate the FORWARD direction relative to the camera
        Vector3 forwardDirection = Camera.main.transform.forward;
        forwardDirection.y = 0; // Remove vertical movement

        // Calculate the RIGHT direction relative to the camera
        Vector3 rightDirection = Camera.main.transform.right;

        // Combine the forward and right direction into a single vector
        Vector3 movementDirection = forwardDirection * verticalInput + rightDirection * horizontalInput;
        movementDirection.Normalize(); // Ensure the direction vector has a length of 1

        // Apply the movement to the Rigidbody
        Vector3 movement = movementDirection * (moveSpeed * Time.deltaTime);
        myRigidbody.velocity = new Vector3(movement.x, myRigidbody.velocity.y, movement.z);
        if (movementDirection != Vector3.zero)
        {
            TurnTowardsMovementDirection(movementDirection);
        }
    }
    
    private void TurnTowardsMovementDirection(Vector3 moveDirection)
    {
        //This will create a rotation that looks in the direction of the moveDirection parameter
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        myRigidbody.rotation = targetRotation;
    }

    public void Jump()
    {
        timeSinceJump += Time.deltaTime;

        if (InputManager.jumpInput == 1)
        {
            myAnimator.SetTrigger("Jump");
            if (myRigidbody.velocity.y < 1 && timeSinceJump > jumpCoolTime)
            {
                myRigidbody.AddForce(0, jumpForce, 0);
                timeSinceJump = 0;

            }
        }
    }

}
