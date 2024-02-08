using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 5;
    [SerializeField] private float yClamp = 60;

    private float xRotation = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 input = InputManager.turnInput;
        // Rotating around the Y-axis (left and right)
        transform.Rotate(Vector3.up * (input.x * sensitivity));

        xRotation -= input.y;
        xRotation = Mathf.Clamp(xRotation, -yClamp, yClamp); // Clamping to prevent over-rotation

        transform.localEulerAngles = new Vector3(xRotation, transform.localEulerAngles.y, 0);
    }
}
