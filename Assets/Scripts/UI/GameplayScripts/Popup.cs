using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour, IPopup
{
    [Header("Popup Components")]
    [SerializeField] GameObject keyboardObj;
    [SerializeField] GameObject controllerObj;


    bool isOn;
    private void Start()
    {
        TurnBothOff();
    }


    private void Update()
    {
        if (isOn)
        {
            DisplayInteractText();
        }
    }

    /// <summary>
    /// From the IPopup interface, sets the active game object to display how to interact to being hidden.
    /// </summary>
    public void DisplayInteractText()
    {
        if (InputManager.Instance.GamepadActive)
        {
            isOn = true;
            DisplayController();
        }
        else if (!InputManager.Instance.GamepadActive)
        {
            isOn = true;
            DisplayKeyboard();
        }
        else
        {
            isOn = false;
        }
    }

    /// <summary>
    /// Turns off the object so the player cannot see it anymore.
    /// </summary>
    public void HideInteractText()
    {
        TurnBothOff();
        isOn = false;
    }

    private void DisplayController()
    {
        controllerObj.SetActive(true);
        keyboardObj.SetActive(false);
    }

    private void DisplayKeyboard()
    {
        controllerObj.SetActive(false);
        keyboardObj.SetActive(true);
    }

    private void TurnBothOff()
    {
        controllerObj.SetActive(false);
        keyboardObj.SetActive(false);
    }
}
