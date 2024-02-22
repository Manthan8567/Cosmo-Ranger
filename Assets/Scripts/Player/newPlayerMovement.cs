using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class newPlayerMovement : MonoBehaviour
{
    [SerializeField] float walkSpeed = 2;
    [SerializeField] float runSpeed = 4;

    Rigidbody _rigidbody;
    Animator _animator;

    // player will rotate according to this angle
    float targetAngle;

    float jumpForce = 200;
    float jumpCoolTime = 1.5f;
    float timeSinceJump = 0;

    float xInput = 0;
    float zInput = 0;


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
        xInput = InputManager.moveInput.x;
        zInput = InputManager.moveInput.y;

        Vector3 direction = new Vector3(xInput, 0f, zInput).normalized;

        _rigidbody.velocity = new Vector3(xInput * walkSpeed, _rigidbody.velocity.y, zInput * walkSpeed);

        // Run
        if (InputManager.runInput == 1)
        {
            _rigidbody.velocity = new Vector3(xInput * runSpeed, _rigidbody.velocity.y, zInput * runSpeed);
        }

        SetMoveAnimation();
        HeadForward(direction);
    }

    private void SetMoveAnimation()
    {
        // Walking
        if (xInput != 0 || zInput != 0)
        {
            // Get player's local velocity
            Vector3 localVelocity = transform.InverseTransformDirection(_rigidbody.velocity);
            // Get forward velocity
            _animator.SetFloat("Speed", localVelocity.z);
        }

        // Idle
        else
        {
            _animator.SetFloat("Speed", 0);
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        // When player landed from jump
        float yNormal = collision.contacts[0].normal.y;

        if (yNormal > 0.7f)
        {
            _animator.SetTrigger("Landed");
        }
    }
}
