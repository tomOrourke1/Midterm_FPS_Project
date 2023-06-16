using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] bool inTransition;
    [SerializeField, Range(0.1f, 10f)] float showDistance;

    bool displayingPopup;
    IPopup lastPopup;

    private void Start()
    {
        displayingPopup = false;
    }
    // Update is called once per frame
    void Update()
    {
        ShowPopup();

        if (Input.GetKeyDown(KeyCode.E) && !inTransition)
        {
            StartCoroutine(ElevatorInteract());
        }
    }

    IEnumerator ElevatorInteract()
    {
        inTransition = true;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, 10000))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
        yield return new WaitForSeconds(1f);
        inTransition = false;
    }

    /// <summary>
    /// Runs constantly in update to always check if the player is looking at a popup-able game object
    /// </summary>
    private void ShowPopup()
    {
        // RaycastHit variable to store what the camera is looking at.
        RaycastHit hit;
        // Runs a raycast to the screens center (where the camera is looking) and outputs the 'hit'
        // with a float storing how far you have to be to see the object appear. (Make it a serialize field/public)
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, showDistance))
        {
            // Stores the popup interface from the collider by getting the component
            IPopup popup = hit.collider.GetComponent<IPopup>();

            // If the popup is not null, meaning that there is a popup and its not currently displaying another popup
            if (popup != null && !displayingPopup)
            {
                // Stores the last popup in one a buffer IPopup variable
                // Sets the displaying popup to true so we cannot enter this again
                lastPopup = popup;
                displayingPopup = true;
                // Sets the interactive text to be active
                popup.DisplayInteractText();
            }
            // If the current popup is null and the last popup is not null then we can hide the text and then flip the bool
            else if (popup == null && lastPopup != null)
            {
                lastPopup.HideInteractText();
                displayingPopup = false;
            }
        }
        // Otherwise if we are not displayinga up
        else if (displayingPopup)
        {
            // Null check on the last popup (? operator)
            // and if its not null then run the HideInteractText (which is off an interface so we can run that)
            lastPopup?.HideInteractText();
            // Set the displaying popup check to false so we can then hover over more displayers
            displayingPopup = false;
        }
    }
}
