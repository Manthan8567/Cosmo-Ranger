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
    public static float punchInput;
    public static float spell_fireballInput;


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
        myInputReader.Player.Punch.performed += ctx => punchInput = ctx.ReadValue<float>();
        myInputReader.Player.Punch.canceled += ctx => punchInput = ctx.ReadValue<float>();

        // Player spell -fireball input
        myInputReader.Player.Spell_Fireball.performed += ctx => spell_fireballInput = ctx.ReadValue<float>();
        myInputReader.Player.Spell_Fireball.canceled += ctx => spell_fireballInput = ctx.ReadValue<float>();


        myInputReader.Player.Enable();
    }

}
