using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerAnimatorController : MonoBehaviour
{
    Animator _animator;
    Rigidbody _rigidbody;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Locomotion();
    }

    public void Locomotion()
    {
        // Walking
        if (InputManager.moveInput.x != 0 || InputManager.moveInput.y != 0)
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
