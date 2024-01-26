using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject camTarget;

    float maxAngle = 30;


    void Update()
    {
        Debug.Log(InputManager.lookAroundInput);

        if (camTarget.transform.eulerAngles.y < maxAngle && camTarget.transform.eulerAngles.y > -maxAngle)
        {
            float clampedAngle = Mathf.Clamp(InputManager.lookAroundInput, -30, 30);
            
            camTarget.transform.Rotate(0, clampedAngle, 0);
        }
        //Debug.Log(camTarget.transform.eulerAngles.y);
    }
}
