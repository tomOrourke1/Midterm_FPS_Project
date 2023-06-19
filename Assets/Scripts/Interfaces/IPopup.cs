using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPopup
{
    /// <summary>
    /// Looking at the interactable item should display how to interact with the object.
    /// </summary>
    public void DisplayInteractText();

    /// <summary>
    /// To turn off the popup text when we are not looking at it.
    /// </summary>
    public void HideInteractText();
}
