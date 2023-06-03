using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RadialMenu_V2 : MonoBehaviour
{
    [Header("Components")]
    [Tooltip("The Radial weapon wheel UI to toggle.")]
    [SerializeField] GameObject radialUI;
    [Tooltip("The arrow to adjust to look at the mouse.")]
    [SerializeField] Transform arrow;
    [SerializeField] GameObject playerStatsUI;
    [SerializeField] GameObject reticleUI;

    bool isMenuBeingShown;
    Vector2 mousePos;
    float rotateAngle;

    private void Start()
    {
        // On the first frame if the radial menu is left on
        // in the inspector, turn it off no matter what. So it doesn't show.
        // then when the player presses Q it will show up again in the update funciton.
        isMenuBeingShown = false;
        radialUI.SetActive(isMenuBeingShown);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleMenu();
        }

        if (isMenuBeingShown)
        {
            UpdateMousePosition();
        }
    }

    void ToggleMenu()
    {
        isMenuBeingShown = !isMenuBeingShown;

        // Turn off Player Stats UI and Reticle from being shown if
        // the radial menu is to be shown.
        playerStatsUI.SetActive(!isMenuBeingShown);
        reticleUI.SetActive(!isMenuBeingShown);
        radialUI.SetActive(isMenuBeingShown);

        // Set the cursor to the opposite of the menu's visiblity
        Cursor.visible = !isMenuBeingShown;
    }

    void UpdateMousePosition()
    {
        mousePos = Input.mousePosition;
        rotateAngle = Vector2.SignedAngle(Vector2.up, mousePos);
        //Debug.Log(rotateAngle);
        arrow.rotation = Quaternion.Euler(0, 0, rotateAngle);

        // Continuous rotation of the angle by time. (Like a spinner)
        // arrow.Rotate(Vector3.forward, rotateAngle * Time.deltaTime);
    }
}
