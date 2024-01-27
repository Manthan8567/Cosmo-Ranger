using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject camTarget;

    float sensitivity = 0.1f;
    float maxAngle = 150;
    float rotationValue = 0;


    void Update()
    {
        rotationValue += InputManager.lookAroundInput;
        rotationValue = Mathf.Clamp(rotationValue, -maxAngle, maxAngle);

        camTarget.transform.localEulerAngles = new Vector3(0, rotationValue * sensitivity, 0);
    }
}
