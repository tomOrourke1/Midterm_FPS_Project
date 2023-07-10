using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfographicButtonUpdate : MonoBehaviour
{
    [Header("Popup Components")]
    [SerializeField] GameObject keyboardObj;
    [SerializeField] GameObject controllerObj;

    private void Update()
    {
        DisplayInteractText();
    }

    public void DisplayInteractText()
    {
        if (InputManager.Instance.GamepadActive)
        {
            DisplayController();
        }
        else if (!InputManager.Instance.GamepadActive)
        {
            DisplayKeyboard();
        }
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
}
