using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    InputReader myInputReader;

    public static Vector2 moveInput;
    public static float lookAroundInput;


    private void Awake()
    {
        myInputReader = new InputReader();
    }

    private void OnEnable()
    {
        myInputReader.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        myInputReader.Player.Move.canceled += ctx => moveInput = ctx.ReadValue<Vector2>();

        myInputReader.Camera.LookAround.performed += ctx => lookAroundInput = ctx.ReadValue<float>();
        myInputReader.Camera.LookAround.performed += ctx => lookAroundInput = ctx.ReadValue<float>();

        myInputReader.Player.Enable();
        myInputReader.Camera.Enable();
    }
}
