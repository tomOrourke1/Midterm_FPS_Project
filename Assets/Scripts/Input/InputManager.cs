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
    bool isRadialShowing;

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


        isRadialShowing = false;
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
        //if (UIManager.instance.currentState == MenuState.none && !GameManager.instance.AllKinesisDisabled())
        //{
        //    UIManager.instance.ShowRadialMenu();
        //    isRadialShowing = true;
        //}
    }

    private void OnRadClose(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //if (UIManager.instance.currentState == MenuState.radial)
        //{
        //    UIManager.instance.HideRadialMenu();
        //    UIManager.instance.UpdateKinesis();
        //    isRadialShowing = false;
        //}
    }

    private void OnEscape(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        UIManager.instance.uiStateMachine.SetOnEscape(true);

        //if (UIManager.instance.currentState == MenuState.none)
        //{
        //    UIManager.instance.PauseGame();
        //}
        //else if (UIManager.instance.currentState == MenuState.paused)
        //{
        //    UIManager.instance.Unpaused();
        //}
        //else if (UIManager.instance.currentState == MenuState.cheats)
        //{
        //    UIManager.instance.CloseCheatMenu();
        //}
    }

    private void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //if (UIManager.instance.currentState == MenuState.infographic)
        //{
        //    UIManager.instance.CloseInfoUI();
        //}
    }

    private void Update()
    {
        //if (UIManager.instance.currentState == MenuState.radial && isRadialShowing)
        //{
        //    UIManager.instance.UpdateRadialWheel();
        //}
    }



}
