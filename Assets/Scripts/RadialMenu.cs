using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    [Header("Components")]
    [Tooltip("The Radial Weapon Wheel UI to toggle. This is the \"RadialMenu\" Game Object prefab and should be under the \"UI\" Game Object.")]
    [SerializeField] GameObject radialUI;
    [Tooltip("The reticle prefab can be found in the \"Prefabs\" Folder and should be placed under the \"UI\" Game Object.")]
    [SerializeField] GameObject reticleUI;
    [Tooltip("The Translucent Background to tint the screen. This a Game Object prefab that should be placed under the \"UI\".")]
    [SerializeField] GameObject translucentBackground;
    [Tooltip("Displays the current hovered Kinesis in the Radial Menu. The Info Box is found under the \"RadialMenu\" Game Object prefab.")]
    [SerializeField] TextMeshProUGUI infoBox;
    [Tooltip("The arrow rotating around to face the mouse's position. This is found under the \"RadialMenu\" Game Object prefab.")]
    [SerializeField] Transform arrow;
    [Tooltip("The Radial Slices of each Kinesis Type. This is found under the \"RadialMenu\" Game Object prefab.")]
    [SerializeField] GameObject[] slices;
    [Tooltip("A transparent selector to higlight which item is being hovered on. This is found under the \"RadialMenu\" Game Object prefab.")]
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
        translucentBackground.SetActive(isMenuBeingShown);
    }

    void ToggleMenu()
    {
        isMenuBeingShown = !isMenuBeingShown;
        reticleUI.SetActive(!isMenuBeingShown);
        translucentBackground.SetActive(isMenuBeingShown);
        radialUI.SetActive(isMenuBeingShown);
    }

    void UpdateMousePosition()
    {

        var screenPos = new Vector3(Screen.width / 2, Screen.height / 2);
        mousePos = Input.mousePosition - screenPos;

        var scale = mousePos.magnitude;


        rotateAngle = Vector2.SignedAngle(Vector2.up, mousePos);
        rotateAngle = rotateAngle < 0 ? rotateAngle + 360 : rotateAngle;
        arrow.rotation = Quaternion.Euler(0, 0, rotateAngle);

        arrow.localScale = new Vector3(1, scale, 1);
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
                DisplayKinesisInRadialMenu(sliceIndex);
                //Debug.Log(trackedKinesis);
            }
        }
    }

    /// <summary>
    /// Provides functionality to run functions to set which gun is in use.
    /// Pass in the selected slice and it will set the current gun to that type of kinesis.
    /// </summary>
    /// <param name="sliceIndx">The indexed slice to use.</param>
    void SelectKinesis(int sliceIndx)
    {
        switch (sliceIndx)
        {
            case 0:
                // Functionality To Select the Kinesis Type will be put in here. For now this is temporary.
                break;
            case 1:
                // Functionality To Select the Kinesis Type will be put in here. For now this is temporary.
                break;
            case 2:
                // Functionality To Select the Kinesis Type will be put in here. For now this is temporary.
                break;
            case 3:
                // Functionality To Select the Kinesis Type will be put in here. For now this is temporary.
                break;
            case 4:
                // Functionality To Select the Kinesis Type will be put in here. For now this is temporary.
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Purely for displaying the type of kinesis in the Radial Menu.
    /// Not for getting the gun type.
    /// </summary>
    /// <param name="idx">The index of the kinesis type to use.</param>
    void DisplayKinesisInRadialMenu(int idx)
    {
        switch (idx)
        {
            case 0:
                infoBox.SetText(slices[idx].GetComponent<RadialSliceName>().weaponName);
                break;
            case 1: 
                infoBox.SetText(slices[idx].GetComponent<RadialSliceName>().weaponName);
                break;  
            case 2:
                infoBox.SetText(slices[idx].GetComponent<RadialSliceName>().weaponName);
                break;
            case 3:
                infoBox.SetText(slices[idx].GetComponent<RadialSliceName>().weaponName);
                break;
            case 4:
                infoBox.SetText(slices[idx].GetComponent<RadialSliceName>().weaponName);
                break;

            default:
                break;
        }
    }

    public void UpdateKeys()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleMenu();
            GameObject.Find("Player").GetComponentInChildren<CameraController>().enabled = !isMenuBeingShown;
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            ToggleMenu();
            GameObject.Find("Player").GetComponentInChildren<CameraController>().enabled = !isMenuBeingShown;
            SelectKinesis(trackedKinesis);
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKey(KeyCode.Q) && isMenuBeingShown)
        {
            UpdateMousePosition();
            UpdateSelectedItem();
        }
    }
}
