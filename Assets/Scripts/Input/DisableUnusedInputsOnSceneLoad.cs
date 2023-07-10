using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUnusedInputsOnSceneLoad : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] bool moveInput;
    [SerializeField] bool lookDeltaInput;
    [SerializeField] bool lookVectorInput;
    [SerializeField] bool mousePosInput;
    [SerializeField] bool fireInput;
    [SerializeField] bool kinesisInput;
    [SerializeField] bool dashInput;
    [SerializeField] bool crouchInput;
    [SerializeField] bool crouchToggleInput;
    [SerializeField] bool jumpInput;
    [SerializeField] bool meleeInput;
    [SerializeField] bool radialWheelInput;
    [SerializeField] bool escapeInput;
    [SerializeField] bool interactInput;
    [SerializeField] bool anyInput;
    [SerializeField] bool joyXInput;
    [SerializeField] bool joyYInput;
    [SerializeField] bool uiSelectInput;

    private void Start()
    {
        EnableDisableInputs();
    }

    private void OnDestroy()
    {
        moveInput = true;
        lookDeltaInput = true;
        lookVectorInput = true;
        mousePosInput = true;
        fireInput = true;
        kinesisInput = true;
        dashInput = true;
        crouchInput = true;
        crouchToggleInput = true;
        jumpInput = true;
        meleeInput = true;
        radialWheelInput = true;
        escapeInput = true;
        interactInput = true;
        anyInput = true;
        joyXInput = true;
        joyYInput = true;
        uiSelectInput = true;
        EnableDisableInputs();
    }

    private void EnableDisableInputs()
    {
        if (moveInput) { InputManager.Instance.Action.Move.Enable(); } else { InputManager.Instance.Action.Move.Disable(); }
        if (lookDeltaInput) { InputManager.Instance.Action.LookDelta.Enable(); } else { InputManager.Instance.Action.LookDelta.Disable(); }
        if (lookVectorInput) { InputManager.Instance.Action.lookVector.Enable(); } else { InputManager.Instance.Action.lookVector.Disable(); }
        if (mousePosInput) { InputManager.Instance.Action.MousePos.Enable(); } else { InputManager.Instance.Action.MousePos.Disable(); }
        if (fireInput) { InputManager.Instance.Action.Fire.Enable(); } else { InputManager.Instance.Action.Fire.Disable(); }
        if (kinesisInput) { InputManager.Instance.Action.Kinesis.Enable(); } else { InputManager.Instance.Action.Kinesis.Disable(); }
        if (dashInput) { InputManager.Instance.Action.Dash.Enable(); } else { InputManager.Instance.Action.Dash.Disable(); }
        if (crouchInput) { InputManager.Instance.Action.Crouch.Enable(); } else { InputManager.Instance.Action.Crouch.Disable(); }
        if (crouchToggleInput) { InputManager.Instance.Action.CrouchToggle.Enable(); } else { InputManager.Instance.Action.CrouchToggle.Disable(); }
        if (jumpInput) { InputManager.Instance.Action.Jump.Enable(); } else { InputManager.Instance.Action.Jump.Disable(); }
        if (meleeInput) { InputManager.Instance.Action.Melee.Enable(); } else { InputManager.Instance.Action.Melee.Disable(); }
        if (radialWheelInput) { InputManager.Instance.Action.OpenRadialWheel.Enable(); } else { InputManager.Instance.Action.OpenRadialWheel.Disable(); }
        if (escapeInput) { InputManager.Instance.Action.Escape.Enable(); } else { InputManager.Instance.Action.Escape.Disable(); }
        if (interactInput) { InputManager.Instance.Action.Interact.Enable(); } else { InputManager.Instance.Action.Interact.Disable(); }
        if (anyInput) { InputManager.Instance.Action.any.Enable(); } else { InputManager.Instance.Action.any.Disable(); }
        if (joyXInput) { InputManager.Instance.Action.JoyX.Enable(); } else { InputManager.Instance.Action.JoyX.Disable(); }
        if (joyYInput) { InputManager.Instance.Action.JoyY.Enable(); } else { InputManager.Instance.Action.JoyY.Disable(); }
        if (uiSelectInput) { InputManager.Instance.Action.UISelect.Enable(); } else { InputManager.Instance.Action.UISelect.Disable(); }

    }
}
