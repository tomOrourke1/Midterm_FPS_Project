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


        InputSystem.onDeviceChange += ChangeDevice;
        pInput.onControlsChanged += ControlsChanged;

    }

    private void ControlsChanged(PlayerInput input)
    {
        //Debug.LogError("Controls changed");
    }

    private void ChangeDevice(InputDevice device, InputDeviceChange change)
    {

        //if (device is Gamepad)
        //{
        //    Debug.LogError("Device is gamepad");
        //}
        //if (device is Keyboard)
        //{
        //    Debug.LogError("Device is keyboard");
        //}

        //switch (change)
        //{
        //    case InputDeviceChange.Added:
        //        Debug.LogError($"{device.name} added");
        //        break;
        //    case InputDeviceChange.Removed:
        //        Debug.LogError($"{device.name} removed");
        //        break;
        //    case InputDeviceChange.Disconnected:
        //        Debug.LogError($"{device.name} disconnected");
        //        break;
        //    case InputDeviceChange.Reconnected:
        //        Debug.LogError($"{device.name} reconnected");
        //        break;
        //    case InputDeviceChange.Enabled:
        //        Debug.LogError($"{device.name} enabled");
        //        break;
        //    case InputDeviceChange.Disabled:
        //        Debug.LogError($"{device.name} disabled");
        //        break;
        //    case InputDeviceChange.UsageChanged:
        //        Debug.LogError($"{device.name} useage changed");
        //        break;
        //    case InputDeviceChange.ConfigurationChanged:
        //        Debug.LogError($"{device.name} configuration changed");
        //        break;
        //    case InputDeviceChange.SoftReset:
        //        Debug.LogError($"{device.name} soft reset");
        //        break;
        //    case InputDeviceChange.HardReset:
        //        Debug.LogError($"{device.name} hard reset");
        //        break;
        //    case InputDeviceChange.Destroyed:
        //        Debug.LogError($"{device.name} destroyed");
        //        break;
        //    default:
        //        Debug.LogError($"{device.name} NO CASE HERE????");
        //        break;
        //}


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
        UIManager.instance?.uiStateMachine.SetInteract(true);
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
