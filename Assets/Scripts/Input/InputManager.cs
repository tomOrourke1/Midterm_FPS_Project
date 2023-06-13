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
        input.Player.OpenRadialWheel.performed += OnRadMenu;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        input.Player.Escape.performed -= OnEscape;
        input.Player.OpenRadialWheel.performed -= OnRadMenu;
    }

    private void OnRadMenu(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.action.inProgress)
        {
            UIManager.instance.UpdateRadialWheel();
        }
        else if (context.action.WasPressedThisFrame())
        {
            UIManager.instance.ShowRadialMenu();
        }
        else if (context.action.WasReleasedThisFrame())
        {
            UIManager.instance.HideRadialMenu();
        }
    }

    private void OnEscape(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        UIManager.instance.PauseGame();
    }



    private void Update()
    {

    }


}
