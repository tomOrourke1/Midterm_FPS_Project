using System.Runtime.InteropServices.WindowsRuntime;
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
    [SerializeField] string sliceName;
    [SerializeField] Sprite kinesisIcon;

    public GameObject GetSlice()
    {
        return slice;
    }

    public void SetSliceMaterial(Material material)
    {
        slice.GetComponent<Image>().material = material;
    }

    public string GetName()
    {
        return sliceName;
    }

    public Sprite GetIcon()
    {
        return kinesisIcon;
    }
}

public class RadialMenu : MonoBehaviour
{
    [Header("UI Toggles")]
    [Tooltip("The Radial Weapon Wheel UI to toggle. This is the \"RadialMenu\" Game Object prefab and should be under the \"UI\" Game Object.")]
    [SerializeField] GameObject radialUI;
    [Tooltip("The reticle prefab can be found in the \"Prefabs\" Folder and should be placed under the \"UI\" Game Object.")]
    [SerializeField] GameObject reticleUI;
    [Tooltip("The arrows game object to turn off when the start function is called.")]
    [SerializeField] GameObject arrowObj;
    [Tooltip("The Translucent Background to tint the screen. This a Game Object prefab that should be placed under the \"UI\".")]
    [SerializeField] public GameObject translucentBackground;

    [Header("Text Display")]
    [Tooltip("Displays the current hovered Kinesis in the Radial Menu. The Info Box is found under the \"RadialMenu\" Game Object prefab.")]
    [SerializeField] TextMeshProUGUI infoBox;

    [Header("Transforms")]
    [Tooltip("The arrow rotating around to face the mouse's position. This is found under the \"RadialMenu\" Game Object prefab.")]
    [SerializeField] Transform arrow;
    [Tooltip("A transparent selector to higlight which item is being hovered on. This is found under the \"RadialMenu\" Game Object prefab.")]
    [SerializeField] Transform selector;
    [Tooltip("A list of each radial option, and if they are enabled or not.")]

    [Header("Slices Struct")]
    [SerializeField] SlicesStruct[] _slices;
    [Tooltip("Disabled color that the slice will be set to. The slice meaning the radial slice in the radial menu.")]

    [Header("Color Settings")]
    [SerializeField] Material disabledColor;
    [Tooltip("Enabled color that the slice will be set to. The slice meaning the radial slice in the radial menu.")]
    [SerializeField] Material enabledColor;

    [Header("Audio Source / Tick")]
    [SerializeField] AudioSource audSource;
    [SerializeField] AudioClip tickSFX;

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

    /// <summary>
    /// How much to offset the selector icon.
    /// </summary>
    float offsetAngle;

    /// <summary>
    /// Uses the last selected kinesis to confirm the selection.
    /// </summary>
    int confirmedKinesis = 2;

    /// <summary>
    /// Plays the sfx for ticking only once.
    /// </summary>
    bool playSFX;

    /// <summary>
    /// Current vs Previous Highlight
    /// </summary>
    int prevKinesis;
    #endregion

    private void Start()
    {
        sliceAng = 360 / _slices.Length;
        offsetAngle = sliceAng * 3;
        UpdateSlices();
    }

    /// <summary>
    /// Turns the radial menu to the ON state.
    /// </summary>
    public void ShowRadialMenu()
    {
        ChangeConfirmedWhenOpeningWheel();
        UpdateSlices();

        Cursor.visible = false;

        selector.gameObject.SetActive(true);
        arrowObj.SetActive(false);
        reticleUI.SetActive(false);
        translucentBackground.SetActive(true);
        radialUI.SetActive(true);
    }

    /// <summary>
    /// Turns the radial menu to the OFF state.
    /// </summary>
    public void HideRadialMenu()
    {
        reticleUI.SetActive(true);
        translucentBackground.SetActive(false);
        radialUI.SetActive(false);
        UpdateSlices();
    }

    /// <summary>
    /// Updates the position of the mouse to where the mouse is on the canvas.
    /// </summary>
    private void UpdateMousePosition()
    {
        var screenPos = new Vector3(Screen.width / 2, Screen.height / 2);
        //mousePos = Input.mousePosition - screenPos;
        mousePos = (Vector3)InputManager.Instance.Action.MousePos.ReadValue<Vector2>() - screenPos;

        arrowScale = mousePos.magnitude / 75;
        if (arrowScale >= maxArrowDist)
            arrowScale = maxArrowDist;

        rotateAngle = Vector2.SignedAngle(Vector2.up, mousePos);
        rotateAngle = rotateAngle < 0 ? rotateAngle + 360 : rotateAngle;

        arrow.rotation = Quaternion.Euler(0, 0, rotateAngle);
        arrow.localScale = new Vector3(1, arrowScale, 1);
    }

    /// <summary>
    /// Updates the selector wheel icon to position and rotate itself on top of the currently
    /// highlighted item that can be enabled.
    /// </summary>
    private void UpdateSelectedItem()
    {
        UpdateSlices();

        for (int sliceIndex = 0; sliceIndex < _slices.Length; ++sliceIndex)
        {
            withinRadialMin = rotateAngle > sliceIndex * sliceAng;
            withinRadialMax = rotateAngle < (sliceIndex + 1) * sliceAng;

            if (withinRadialMin && withinRadialMax && GameManager.instance.GetEnabledList().RetrieveLoop(sliceIndex))
            {
                selector.transform.rotation = Quaternion.Euler(0, 0, sliceIndex * sliceAng + (72 / 2));
                trackedKinesis = sliceIndex;
                DisplayKinesisInRadialMenu(sliceIndex);
            }
        }
    }

    /// <summary>
    /// Provides functionality to run functions to set which gun is in use.
    /// </summary>
    public void SelectKinesis()
    {
        UIManager.instance.GetPlayerStats().SetKinesisIcon(_slices[confirmedKinesis].GetIcon());
        GameManager.instance.GetPlayerObj().GetComponent<CasterScript>().SetCurrentKinesis(confirmedKinesis);
    }

    /// <summary>
    /// Purely for displaying the type of kinesis in the Radial Menu.
    /// Not for getting the gun type.
    /// </summary>
    /// <param name="idx">The index of the kinesis type to use.</param>
    private void DisplayKinesisInRadialMenu(int idx)
    {
        infoBox.SetText(_slices[idx].GetName());
        arrowObj.SetActive(true);

        // Updates the tracked kinesis only when it can be displayed so we don't update it when its being hovered over.
        // Meaning we need to reach this function (which can only happen if the slice is also enabled) to even get
        // into the slices and update what radial option slice we chose.
        confirmedKinesis = idx;
        selector.transform.rotation = Quaternion.Euler(0, 0, idx * sliceAng + (sliceAng * 1) - offsetAngle);

        // Play the tick SFX as a one shot from the audio source
        
        if (playSFX)
        {
            audSource.PlayOneShot(tickSFX);
            playSFX = false;
            prevKinesis = confirmedKinesis;
        }

        if (prevKinesis != confirmedKinesis)
        {
            playSFX = true;
        }
        

    }

    public void UpdateSlices()
    {
        for (int sliceIndex = 0; sliceIndex < _slices.Length; ++sliceIndex)
        {
            if (GameManager.instance.GetEnabledList().RetrieveLoop(sliceIndex))
                _slices[sliceIndex].GetSlice().GetComponent<Image>().material = enabledColor;
            else
                _slices[sliceIndex].GetSlice().GetComponent<Image>().material = disabledColor;
        }
    }

    /// <summary>
    /// Runs the 3 functions to
    /// 1. Update the Slices.
    /// 2. Update where the mouse is positioned at.
    /// 3. Update which selected slice you are highlighting.
    /// </summary>
    public void SelectSlice()
    {
        UpdateSlices();
        UpdateMousePosition();
        UpdateSelectedItem();
    }

    /// <summary>
    /// Returns the confirmed kinesis type.
    /// </summary>
    /// <returns></returns>
    public int GetConfirmedKinesis()
    {
        return confirmedKinesis;
    }

    /// <summary>
    /// Returns the Reticle UI game object so we can disable/enable this when pause/death happens
    /// </summary>
    /// <returns></returns>
    public GameObject GetReticle()
    {
        return reticleUI;
    }

    /// <summary>
    /// Gets the last available slice in the radial wheel. If there are none then the wheel will not open.
    /// </summary>
    private void ChangeConfirmedWhenOpeningWheel()
    {
        for (int i = 0; i < _slices.Length; i++)
        {
            if (GameManager.instance.GetEnabledList().RetrieveLoop(i))
            {
                confirmedKinesis = i;
            }
        }
    }
}
