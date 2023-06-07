using System.Xml.Schema;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
struct SlicesStruct
{
    [Tooltip("The Radial Slices of each Kinesis Type. This is found under the \"RadialMenu\" Game Object prefab.")]
    [SerializeField] GameObject slice;
    [Tooltip("If the slice is enabled or disabled.")]
    [SerializeField] bool sliceEnabled;
    [SerializeField] string sliceName;

    public GameObject GetSlice()
    {
        return slice;
    }

    public bool GetBool()
    {
        return sliceEnabled;
    }

    public void SetSliceMaterial(Material material)
    {
        slice.GetComponent<Image>().material = material;
    }

    public string GetName()
    {
        return sliceName;
    }
}

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
    [Tooltip("A transparent selector to higlight which item is being hovered on. This is found under the \"RadialMenu\" Game Object prefab.")]
    [SerializeField] Transform selector;
    [Tooltip("A list of each radial option, and if they are enabled or not.")]
    [SerializeField] SlicesStruct[] _slices;
    [Tooltip("Disabled color that the slice will be set to. The slice meaning the radial slice in the radial menu.")]
    [SerializeField] Material disabledColor;
    [Tooltip("Enabled color that the slice will be set to. The slice meaning the radial slice in the radial menu.")]
    [SerializeField] Material enabledColor;
    [SerializeField, Range(75, 500)] int sliceDistanceFromCenter;
    [SerializeField] Transform sliceParentTransform;

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

    /// <summary>
    /// Maximum distance from the center that the arrow in the radial menu can go.
    /// </summary>
    [SerializeField] float maxArrowDist;

    /// <summary>
    /// Scale that the arrow will get the magnitude from the mouses position from the center of the screen.
    /// </summary>
    float arrowScale;

    #endregion

    float timescaleOriginal;

    private void Start()
    {
        sliceAng = 360 / _slices.Length;
        GenerateSlices();
        UpdateSlices();
        // On the first frame if the radial menu is left on
        // in the inspector, turn it off no matter what. So it doesn't show.
        // then when the player presses Q it will show up again in the update funciton.
        isMenuBeingShown = false;
        radialUI.SetActive(isMenuBeingShown);
        translucentBackground.SetActive(isMenuBeingShown);
        timescaleOriginal = Time.timeScale;
    }

    void ToggleMenu()
    {
        UpdateSlices();
        isMenuBeingShown = !isMenuBeingShown;
        reticleUI.SetActive(!isMenuBeingShown);
        translucentBackground.SetActive(isMenuBeingShown);
        radialUI.SetActive(isMenuBeingShown);
        if (isMenuBeingShown)
            Time.timeScale = 0;
        else
            Time.timeScale = timescaleOriginal;
    }

    void UpdateMousePosition()
    {
        var screenPos = new Vector3(Screen.width / 2, Screen.height / 2);
        mousePos = Input.mousePosition - screenPos;

        arrowScale = mousePos.magnitude / 75;
        if (arrowScale >= maxArrowDist)
            arrowScale = maxArrowDist;

        rotateAngle = Vector2.SignedAngle(Vector2.up, mousePos);
        rotateAngle = rotateAngle < 0 ? rotateAngle + 360 : rotateAngle;
        arrow.rotation = Quaternion.Euler(0, 0, rotateAngle);

        arrow.localScale = new Vector3(1, arrowScale, 1);
    }

    void UpdateSelectedItem()
    {
        for (int sliceIndex = 0; sliceIndex < _slices.Length; ++sliceIndex)
        {
            // Checks to see if the current slice is within the minimum or maximum angles
            // of the slice. 
            withinRadialMin = rotateAngle > sliceIndex * sliceAng;
            withinRadialMax = rotateAngle < (sliceIndex + 1) * sliceAng;

            if (withinRadialMin && withinRadialMax && _slices[sliceIndex].GetBool())
            {
                DisplayKinesisInRadialMenu(sliceIndex);
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
        infoBox.SetText(_slices[idx].GetName());
        // Updates the tracked kinesis only when it can be displayed so we don't update it when its being hovered over.
        // Meaning we need to reach this function (which can only happen if the slice is also enabled) to even get
        // into the slices and update what radial option slice we chose.
        trackedKinesis = idx;
        selector.transform.rotation = Quaternion.Euler(0, 0, idx * sliceAng + (sliceAng * 3));
        //Debug.Log("Tracked Kinesis " + trackedKinesis);
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

    void GenerateSlices()
    {
        for (int sliceIndex = 0; sliceIndex < _slices.Length; ++sliceIndex)
        {
            Quaternion rot = Quaternion.Euler(0, 0, (sliceIndex + 1) * sliceAng);

            float xPos, yPos;
            yPos = sliceDistanceFromCenter * Mathf.Sin(sliceAng * (sliceIndex) * Mathf.Deg2Rad);
            yPos += Screen.height / 2;
            xPos = sliceDistanceFromCenter * Mathf.Cos(sliceAng * (sliceIndex) * Mathf.Deg2Rad);
            xPos += Screen.width / 2;

            Instantiate(_slices[sliceIndex].GetSlice(), new Vector3(xPos, yPos, 0), rot, sliceParentTransform);
        }
    }

    void UpdateSlices()
    {
        for (int sliceIndex = 0; sliceIndex < _slices.Length; ++sliceIndex)
        {
            if (_slices[sliceIndex].GetBool())
                _slices[sliceIndex].GetSlice().GetComponent<Image>().material = enabledColor;
            else
                _slices[sliceIndex].GetSlice().GetComponent<Image>().material = disabledColor;

        }
    }
}
