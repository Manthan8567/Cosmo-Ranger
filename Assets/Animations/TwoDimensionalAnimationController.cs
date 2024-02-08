using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalAnimationController : MonoBehaviour
{
    Animator animator;
    float zSpeed = 0.0f;
    float xSpeed = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkSpeed = 0.5f;
    public float MaximumRunSpeed = 2.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backwardPressed = Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");

        float currentMaxSpeed = runPressed ? MaximumRunSpeed : maximumWalkSpeed;

        changeVelocity( forwardPressed,backwardPressed,  leftPressed,  rightPressed,  runPressed, currentMaxSpeed);
        lockOrResetVelocity(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed, currentMaxSpeed);

        animator.SetFloat("xSpeed", xSpeed);
        animator.SetFloat("zSpeed", zSpeed);
    }

    void changeVelocity(bool forwardPressed,bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxSpeed )
    {
        if (forwardPressed && zSpeed < currentMaxSpeed)
        {
            zSpeed += Time.deltaTime * acceleration;
        }
        if (backwardPressed && zSpeed > -currentMaxSpeed)
        {
            xSpeed -= Time.deltaTime * acceleration;
        }
        if (leftPressed && xSpeed > -currentMaxSpeed)
        {
            xSpeed -= Time.deltaTime * acceleration;
        }
        if (rightPressed && xSpeed < currentMaxSpeed)
        {
            xSpeed += Time.deltaTime * acceleration;
        }
        ///Decrease zSpeed
        if (!forwardPressed && zSpeed > 0.0f)
        {
            zSpeed -= Time.deltaTime * deceleration;
        }
        if (!backwardPressed && zSpeed < 0.0f)
        {
            xSpeed += Time.deltaTime * deceleration;
        }
        if (!leftPressed && xSpeed < 0.0f)
        {
            xSpeed += Time.deltaTime * deceleration;
        }
        if (!rightPressed && xSpeed > 0.0f)
        {
            xSpeed -= Time.deltaTime * deceleration;
        }
    }

    void lockOrResetVelocity(bool forwardPressed,bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxSpeed)
    {
        //reset Speed
        if (!forwardPressed && zSpeed < 0.0f)
        {
            zSpeed = 0.0f;
        }
        //Reset SpeedX
        if (!leftPressed && !rightPressed && xSpeed != 0.0f && (xSpeed > -0.05f && zSpeed < 0.05f))
        {
            xSpeed = 0.0f;
        }

        if (forwardPressed && runPressed && zSpeed > currentMaxSpeed)
        {
            zSpeed = currentMaxSpeed;
        }
        else if (forwardPressed && zSpeed > currentMaxSpeed)
        {
            zSpeed -= Time.deltaTime * deceleration;
            if (zSpeed > currentMaxSpeed && zSpeed < (currentMaxSpeed + 0.05))
            { zSpeed = currentMaxSpeed; }
        }
        else if (forwardPressed && zSpeed < currentMaxSpeed && zSpeed > (currentMaxSpeed - 0.05f))
        {
            zSpeed = currentMaxSpeed;
        }
    }
}
