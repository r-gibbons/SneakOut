using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    static PlayerInput actions;


    public static Action<Vector2> moveAction;
    public static Action<Vector2> lookAction;
    public static Action jumpAction;
    public static Action crouchAction;
    public static Action sprintAction;
    
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            actions = new();
            actions.Enable();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        actions.InGame.Move.performed += InvokeMovement;
        actions.InGame.Move.canceled += InvokeMovement;

        actions.InGame.Sprint.performed += InvokeSprint;
        actions.InGame.Sprint.canceled += InvokeSprint;

        actions.InGame.Look.performed += InvokeLook;
        actions.InGame.Look.canceled += InvokeLook;

        actions.InGame.Jump.performed += InvokeJump;
        actions.InGame.Crouch.performed += InvokeCrouch;
    }
    void InvokeSprint(InputAction.CallbackContext ctx)
    {
        sprintAction?.Invoke();
    }
    void InvokeMovement(InputAction.CallbackContext ctx)
    {
        moveAction?.Invoke(ctx.ReadValue<Vector2>());
    }
    void InvokeLook(InputAction.CallbackContext ctx)
    {
        lookAction?.Invoke(ctx.ReadValue<Vector2>());
    }
    void InvokeJump(InputAction.CallbackContext ctx)
    {
        jumpAction?.Invoke();
    }
    void InvokeCrouch(InputAction.CallbackContext ctx)
    {
        crouchAction?.Invoke();
    }

    private void OnDisable()
    {
        actions.InGame.Move.performed -= InvokeMovement;
        actions.InGame.Move.canceled -= InvokeMovement;

        actions.InGame.Sprint.performed -= InvokeSprint;
        actions.InGame.Sprint.canceled -= InvokeSprint;

        actions.InGame.Look.performed -= InvokeLook;
        actions.InGame.Look.canceled -= InvokeLook;

        actions.InGame.Jump.performed -= InvokeJump;
        actions.InGame.Crouch.performed -= InvokeCrouch;
    }
}
