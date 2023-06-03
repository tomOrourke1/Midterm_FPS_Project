using UnityEngine;
using UnityEngine.UI;

public class RadialMenu_V2 : MonoBehaviour
{
    [Header("Components")]
    [Tooltip("The Radial weapon wheel UI to toggle.")]
    [SerializeField] GameObject radialUI;
    [Tooltip("The arrow to adjust to look at the mouse.")]
    [SerializeField] Transform arrow;
    [Tooltip("The Player Stats UI to enable/disable when turning on/off the Radial Menu.")]
    [SerializeField] GameObject playerStatsUI;
    [Tooltip("The Reticle UI to enable/disable when turning on/off the Radial Menu.")]
    [SerializeField] GameObject reticleUI;
    [SerializeField] GameObject[] slices;
    [Tooltip("A transparent selector to display which item is being hovered on.")]
    [SerializeField] Transform selector;

    #region ScriptVariables
    /// <summary>
    /// The angle between each slice in the radial menu.
    /// </summary>
    int sliceAng;

    /// <summary>
    /// Keeps track of if the radial menu should be displayed or not.
    /// </summary>
    bool isMenuBeingShown;

    /// <summary>
    /// Keeps track of if the angle is above the minimum amount of degrees.
    /// </summary>
    bool withinRadialMin;

    /// <summary>
    /// Keeps track of if the angle is below the minimum amount of degrees
    /// Used in adjusting the selector image.
    /// </summary>
    bool withinRadialMax;

    /// <summary>
    /// Keeps track of the Mouse's position. Used in adjusting the selector image.
    /// </summary>
    Vector2 mousePos;

    /// <summary>
    /// Keeps track of the rotation angle from the mouse to the middle.
    /// Used in adjusting the selector and updating the arrow image
    /// </summary>
    float rotateAngle;

    /// <summary>
    /// Defines what ability is being used currently, and which one to select when leaving the radial menu.
    /// </summary>
    int trackedKinesis;

    #endregion

    private void Start()
    {
        sliceAng = 360 / slices.Length;

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

        if (Input.GetKeyUp(KeyCode.Q))
        {
            ToggleMenu();
            SelectKinesis(trackedKinesis);
        }

        if (Input.GetKey(KeyCode.Q) && isMenuBeingShown)
        {
            UpdateMousePosition();
            UpdateSelectedItem();
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
    }

    void UpdateMousePosition()
    {
        mousePos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2);

        rotateAngle = Vector2.SignedAngle(Vector2.up, mousePos);
        rotateAngle = rotateAngle < 0 ? rotateAngle + 360 : rotateAngle;
        arrow.rotation = Quaternion.Euler(0, 0, rotateAngle);

    }

    void UpdateSelectedItem()
    {
        for (int sliceIndex = 0; sliceIndex < slices.Length; ++sliceIndex)
        {
            // Checks to see if the current slice is within the minimum or maximum angles
            // of the slice. 
            withinRadialMin = rotateAngle > sliceIndex * sliceAng;
            withinRadialMax = rotateAngle < (sliceIndex + 1) * sliceAng;

            if (withinRadialMin && withinRadialMax)
            {
                selector.transform.rotation = Quaternion.Euler(0, 0, sliceIndex * sliceAng + (sliceAng * 3));
                trackedKinesis = sliceIndex;
                Debug.Log(trackedKinesis);
            }
        }
    }

    void SelectKinesis(int _angle)
    {
        switch (_angle)
        {
            case 0:
                Debug.Log("Finger Pistol");
                break;
            case 1:
                Debug.Log("Pyrokinesis");
                break;
            case 2:
                Debug.Log("Cryokinesis");
                break;
            case 3:
                Debug.Log("Aerokinesis");
                break;
            case 4:
                Debug.Log("Electrokinesis");
                break;
            
            default:
                break;
        }
    }
}
