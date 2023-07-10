using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{

    [SerializeField] PlayerInput pInput;

    // instance
    public static InputManager Instance;
    private bool radialShowing;
    public bool canInteract;
    GameInput input;

    bool gamepadActive;

    public bool GamepadActive => gamepadActive;


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


        pInput.onControlsChanged += ControlsChanged;

    }

    private void ControlsChanged(PlayerInput input)
    {
        if(input.currentControlScheme == "Gamepad")
        {
            gamepadActive = true;
        }
        else if(input.currentControlScheme == "KeyboardMouse")
        {
            gamepadActive = false;
        }
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
        if (!GameManager.instance.AllKinesisDisabled())
        {
            UIManager.instance.uiStateMachine.SetRadialAsync(true);
            radialShowing = true;
        }
    }

    private void OnRadClose(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (radialShowing)
        {
            UIManager.instance.uiStateMachine.SetPlay(true);
            radialShowing = false;
        }
    }

    private void OnEscape(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (GameManager.instance.GetCurrentLevel() == "MainMenu")
        {
            MainMenu.instance?.ToggleSettings();
        }
        else
        {
            UIManager.instance?.uiStateMachine?.SetOnEscape(true);
        }
    }

    private void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (canInteract)
        {
            UIManager.instance?.uiStateMachine.SetInteract(true);
            canInteract = false;
        }
    }

    private void Update()
    {
        if (radialShowing)
        {
            UIManager.instance.UpdateRadialWheel();
        }
    }

    public void ChangeInput()
    {

    }
}
