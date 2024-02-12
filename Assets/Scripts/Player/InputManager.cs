using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    InputReader myInputReader;

    public static Vector2 moveInput;
    public static Vector2 turnInput;
    public static float jumpInput;
    public static float runInput;
    public static float attackInput;


    private void Awake()
    {
        myInputReader = new InputReader();
    }

    private void OnEnable()
    {
        // Player movement input
        myInputReader.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        myInputReader.Player.Move.canceled += ctx => moveInput = ctx.ReadValue<Vector2>();

        // Player jump input
        myInputReader.Player.Jump.performed += ctx => jumpInput = ctx.ReadValue<float>();
        myInputReader.Player.Jump.canceled += ctx => jumpInput = ctx.ReadValue<float>();

        // Player turn input
        myInputReader.Player.Turn.performed += ctx => turnInput = ctx.ReadValue<Vector2>();
        myInputReader.Player.Turn.canceled += ctx => turnInput = ctx.ReadValue<Vector2>();

        // Player run input
        myInputReader.Player.Run.performed += ctx => runInput = ctx.ReadValue<float>();
        myInputReader.Player.Run.canceled += ctx => runInput = ctx.ReadValue<float>();

        // Player attack input
        myInputReader.Player.Attack.performed += ctx => attackInput = ctx.ReadValue<float>();
        myInputReader.Player.Attack.canceled += ctx => attackInput = ctx.ReadValue<float>();


        myInputReader.Player.Enable();
    }

}
