using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody myRigidbody;

    [SerializeField] float speed = 10;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        myRigidbody.velocity = new Vector3(InputManager.moveInput.x * speed, myRigidbody.velocity.y, InputManager.moveInput.y * speed);
    }

    
}
