using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void OnEnable()
    {
        input.Player.Escape.performed += OnEscape;
        input.Player.OpenRadialWheel.performed += OnRadMenu;
    }
    private void OnDisable()
    {
        input.Player.Escape.performed -= OnEscape;
        input.Player.OpenRadialWheel.performed -= OnRadMenu;
    }

    private void OnRadMenu(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

    private void OnEscape(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }



    private void Update()
    {

    }


}
