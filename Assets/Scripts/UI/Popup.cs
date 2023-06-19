using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour, IPopup
{
    [Header("Popup Components")]
    [SerializeField] GameObject displayObj;


    /// <summary>
    /// From the IPopup interface, sets the active game object to display how to interact to being hidden.
    /// </summary>
    public void DisplayInteractText()
    {
        if (!displayObj.activeSelf)
        {
            displayObj.SetActive(true);
        }
    }

    /// <summary>
    /// Turns off the object so the player cannot see it anymore.
    /// </summary>
    public void HideInteractText()
    {
        if (displayObj.activeSelf)
        {
            displayObj.SetActive(false);
        }
    }
}
