using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{

    [SerializeField] PlayerInput pInput;

    // instance
    public static InputManager Instance;
    private bool radialShowing;
    public bool canInteract;
    GameInput input;

    bool gamepadActive;
    bool isJoystick;

    public bool GamepadActive => gamepadActive;


    // properties
    public GameInput Input => input;
    public GameInput.PlayerActions Action => input.Player;

    private void Awake()
    {
        Instance = this;


        input = new GameInput();
        input.Player.Enable();

        input.Player.Escape.performed += OnEscape;

        input.Player.OpenRadialWheel.started += OnRadShow;
        input.Player.OpenRadialWheel.canceled += OnRadClose;
        input.Player.Interact.performed += OnInteract;

        pInput.onControlsChanged += ControlsChanged;
    }

    private void Start()
    {

    }

    private void ControlsChanged(PlayerInput input)
    {
        if (input.currentControlScheme == "KeyboardMouse")
        {
            gamepadActive = false;
            isJoystick = false;
        }
        else if (input.currentControlScheme == "Gamepad")
        {
            gamepadActive = true;
            isJoystick = false;
        }
        else if (input.currentControlScheme == "Joystick")
        {
            gamepadActive = true;
            isJoystick = true;
        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            MainMenu.instance.Ping_FixEventSystem();
        }
        else
        {
            UIManager.instance.PingEventSystem();
        }
    }


    public Vector2 GetLookDelta()
    {
        if (isJoystick)
        {
            var x = Action.JoyX.ReadValue<float>();
            var y = -Action.JoyY.ReadValue<float>();
            return new Vector2(x, y);
        }
        else
        {
            return Action.LookDelta.ReadValue<Vector2>();
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
        if (!GameManager.instance.AllKinesisDisabled() && SceneNotMainMenuOrCredits() && !UIManager.instance.GetRadialScript().GetRadialCooldown())
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
        if (SceneManager.GetActiveScene().name == "MainMenu")
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

    private bool SceneNotMainMenuOrCredits()
    {
        if (SceneManager.GetActiveScene().name != "CreditsScene" && SceneManager.GetActiveScene().name != "MainMenu")
            return true;

        return false;
    }
}
