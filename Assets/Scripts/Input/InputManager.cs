using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    // instance
    public static InputManager Instance;
    private bool radialShowing;
    GameInput input;

    // properties
    public GameInput Input => input;
    public GameInput.PlayerActions Action => input.Player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        input = new GameInput();
        input.Player.Enable();

        input.Player.Escape.performed += OnEscape;
        input.Player.OpenRadialWheel.started += OnRadShow;
        input.Player.OpenRadialWheel.canceled += OnRadClose;
        input.Player.Interact.performed += OnInteract;

    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        input.Player.Escape.performed -= OnEscape;
        input.Player.OpenRadialWheel.started -= OnRadShow;
        input.Player.OpenRadialWheel.canceled -= OnRadClose;
        input.Player.Interact.performed -= OnInteract;

    }

    private void OnRadShow(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        UIManager.instance.uiStateMachine.SetRadialAsync(true);
        radialShowing = true;
    }

    private void OnRadClose(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        UIManager.instance.uiStateMachine.SetPlay(true);
        radialShowing = false;
    }

    private void OnEscape(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        UIManager.instance.uiStateMachine.SetOnEscape(true);
    }

    private void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        UIManager.instance.uiStateMachine.SetInteract(true);
    }

    private void Update()
    {
        if (!GameManager.instance.AllKinesisDisabled() && radialShowing)
        {
            UIManager.instance.UpdateRadialWheel();
        }
    }
}
