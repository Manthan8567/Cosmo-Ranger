using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody myRigidbody;

    float moveSpeed = 10;

    float jumpForce = 200;
    float jumpCoolTime = 1;
    float timeSinceJump = 0;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move
        myRigidbody.velocity = new Vector3(InputManager.moveInput.x * moveSpeed, myRigidbody.velocity.y, InputManager.moveInput.y * moveSpeed);

        // Jump
        Jump();
    }

    public void Jump()
    {
        timeSinceJump += Time.deltaTime;

        if (InputManager.jumpInput == 1)
        {
            if (myRigidbody.velocity.y < 1 && timeSinceJump > jumpCoolTime)
            {
                myRigidbody.AddForce(0, jumpForce, 0);
                timeSinceJump = 0;
            }
        }
    }
}
