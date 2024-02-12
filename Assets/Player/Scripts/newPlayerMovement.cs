using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class newPlayerMovement : MonoBehaviour
{
    Rigidbody _rigidbody;
    Animator _animator;

    // player will rotate according to this angle
    float targetAngle;

    float jumpForce = 200;
    float jumpCoolTime = 1.5f;
    float timeSinceJump = 0;

    [SerializeField] float walkSpeed = 2;
    [SerializeField] float runSpeed = 4;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    public void Move()
    {
        // Walk
        float xInput = InputManager.moveInput.x;
        float zInput = InputManager.moveInput.y;

        Vector3 direction = new Vector3(xInput, 0f, zInput).normalized;

        _rigidbody.velocity = new Vector3(xInput * walkSpeed, _rigidbody.velocity.y, zInput * walkSpeed);

        // Run
        if (InputManager.runInput == 1)
        {
            _rigidbody.velocity = new Vector3(xInput * runSpeed, _rigidbody.velocity.y, zInput * runSpeed);
        }

        HeadForward(direction);
    }

    public void HeadForward(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        // To make player to see the direction it's ahead
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

    public void Jump()
    {
        timeSinceJump += Time.deltaTime;

        if (InputManager.jumpInput == 1)
        {
            if (_rigidbody.velocity.y < 1 && timeSinceJump > jumpCoolTime)
            {
                _rigidbody.AddForce(0, jumpForce, 0);
                _animator.SetTrigger("Jump");

                timeSinceJump = 0;
            }
        }
    }
}
