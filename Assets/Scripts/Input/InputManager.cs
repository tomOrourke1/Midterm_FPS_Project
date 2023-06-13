using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    // instance
    public static InputManager Instance;

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
        input.Player.OpenRadialWheel.started += OnRadMenu;
        input.Player.OpenRadialWheel.performed += OnRadUpdate;
        input.Player.OpenRadialWheel.canceled += OnRadClose;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        input.Player.Escape.performed -= OnEscape;
        input.Player.OpenRadialWheel.started -= OnRadMenu;
        input.Player.OpenRadialWheel.performed -= OnRadUpdate;
        input.Player.OpenRadialWheel.canceled -= OnRadClose;

    }

    private void OnRadMenu(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        UIManager.instance.ShowRadialMenu();
    }

    private void OnRadUpdate(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        UIManager.instance.UpdateRadialWheel();
    }

    private void OnRadClose(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        UIManager.instance.HideRadialMenu();
    }

    private void OnEscape(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        UIManager.instance.PauseGame();
    }

    private void Update()
    {

    }


}
