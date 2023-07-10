using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptFacePlayer : MonoBehaviour
{
    private void Update()
    {
        var playerPos = GameManager.instance.GetPlayerPOS();
        var dir = playerPos - transform.position;
        dir.y = 0;
        dir.Normalize();

        transform.localRotation = Quaternion.LookRotation(-dir);
    }

    [Header("Popup Components")]
    [SerializeField] GameObject keyboardObj;
    [SerializeField] GameObject controllerObj;

    private void Start()
    {
        DisplayInteractText();
    }

    /// <summary>
    /// From the IPopup interface, sets the active game object to display how to interact to being hidden.
    /// </summary>
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
        controllerObj?.SetActive(true);
        keyboardObj?.SetActive(false);
    }

    private void DisplayKeyboard()
    {
        controllerObj?.SetActive(false);
        keyboardObj?.SetActive(true);
    }
}
