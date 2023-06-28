
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private FlashDamage flashImageScript;

    [Header("State Machine Reference")]
    [SerializeField] public UIBaseMachine uiStateMachine;

    [Header("Script Reference")]
    [SerializeField] RadialMenu radialScript;
    [SerializeField] PlayerStatsUI statsUIRef;
    [SerializeField] KeyUI keyScriptRef;
    [SerializeField] KeybindsMenu keybinds;
    [SerializeField] LoadingIcon loadingIconScript;

    [Header("Stats UI")]
    [SerializeField] GameObject playerStatsObj;

    [Header("Animator Components")]
    [SerializeField] Animator loseAnimController;

    [Header("Infographics")]
    [SerializeField] GameObject objToShow;
    [SerializeField] Image displayedImage;
    [SerializeField] TextMeshProUGUI textToReplace;

    [Header("Misc Images")]
    [SerializeField] GameObject hitmarker;
    [SerializeField] Image sceneFader;
    [SerializeField] GameObject savingIcon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DisableMenus();
        statsUIRef = GetComponent<PlayerStatsUI>();
        flashImageScript = GetComponent<FlashDamage>();
    }

    /// <summary>
    /// Pauses the game. Shows the cursor and unlocks it.
    /// </summary>
    public void PauseGame()
    {
        GameManager.instance.PauseMenuState();
    }

    /// <summary>
    /// Unpauses the game. Resets the cursor back to being invisible and locked.
    /// </summary>
    public void Unpaused()
    {
        GameManager.instance.PlayMenuState();
    }

    /// <summary>
    /// Runs the ShowRadialMenu function from the radial menu script.
    /// </summary>
    public void ShowRadialMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.instance.TimePause();
        TurnOffCameraScript();
        radialScript.ShowRadialMenu();
    }

    /// <summary>
    /// Runs the HideRadialMenu function from the radial menu script.
    /// </summary>
    public void HideRadialMenu()
    {
        radialScript.HideRadialMenu();
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.TimeUnpause();
        TurnOnCameraScript();
    }

    /// <summary>
    /// Runs the SelectSlice function from the radial menu script.
    /// </summary>
    public void UpdateRadialWheel()
    {
        radialScript.SelectSlice();
    }

    /// <summary>
    /// Closes the settings menu and shows the pause menu. Also sets the ActiveMenu to the pause menu.
    /// </summary>
    public void ApplySettings()
    {
        GameManager.instance.GetSettingsManager().SaveToSettingsObj();
        GameManager.instance.GetSettingsManager().UpdateObjectsToValues();
    }

    /// <summary>
    /// Cancels the users settings and closes the settings menu without saving them to the scriptable object.
    /// </summary>
    public void CancelSettings()
    {
        GameManager.instance.GetSettingsManager().CancelSettingsObj();
        GameManager.instance.GetSettingsManager().CancelSettingsObj();
    }

    /// <summary>
    /// Turns off the player's camera script so the radial menu can update.
    /// Should only be called in the ShowRadialMenu Script.
    /// </summary>
    public void TurnOffCameraScript()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().enabled = false;
    }

    /// <summary>
    /// Turns on the player's camera script so the radial menu can update.
    /// Should only be called in the HideRadialMenu Script.
    /// </summary>
    public void TurnOnCameraScript()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().enabled = true;
    }

    /// <summary>
    /// Runs the lose game logic, also sets the active menu to the lose menu.
    /// Also pauses the game.
    /// </summary>
    public void LoseGame()
    {
        radialScript.GetReticle().SetActive(false);
        playerStatsObj.SetActive(false);

        GameManager.instance.TimePause();
        GameManager.instance.MouseUnlockShow();
    }

    /// <summary>
    /// Sets the | Pause Menu, Win Menu, Lose Menu, Settings Menu, Radial Menu, and Hitmarker (image) |
    /// to false when loading the game initially. Should only be called when the game starts in the UI manager.
    /// </summary>
    private void DisableMenus()
    {
        hitmarker.SetActive(false);
        objToShow.SetActive(false);
        savingIcon.SetActive(false);
    }

    /// <summary>
    /// Returns the player stats script reference.
    /// </summary>
    /// <returns></returns>
    public PlayerStatsUI GetPlayerStats()
    {
        return statsUIRef;
    }

    /// <summary>
    /// Return the scene fader to transition scenes. Should only be used in the elevator script.
    /// </summary>
    /// <returns></returns>
    public Image GetSceneFader()
    {
        return sceneFader;
    }

    /// <summary>
    /// Returns the Hitmarker game object to turn on and off. Used in the Finger Gun script during the Shoot IEnumerator.
    /// </summary>
    /// <returns></returns>
    public GameObject GetHitmarker()
    {
        return hitmarker;
    }

    /// <summary>
    /// Flashes the screen with the shield breaking image.
    /// Should only be used in the PlayerResource script.
    /// </summary>
    public void FlashBreakShield()
    {
        StartCoroutine(flashImageScript.CrackShieldDisplay());
    }

    /// <summary>
    /// Flashes the screen when the player takes damage (that is of thier hp). 
    /// Should only be used in the PlayerResource script.
    /// </summary>
    public void FlashPlayerHealthHit()
    {
        StartCoroutine(flashImageScript.FlashDamageDisplay());
    }

    /// <summary>
    /// Flashes the screen with the shield damage image.
    /// Should only be used in the PlayerResource script.
    /// </summary>
    public void FlashPlayerShieldHit()
    {
        StartCoroutine(flashImageScript.FlashShieldDisplay());
    }

    /// <summary>
    /// Gets the Key UI Script.
    /// </summary>
    /// <returns></returns>
    public KeyUI GetKeyUI()
    {
        return keyScriptRef;
    }

    /// <summary>
    /// Runs the function to update which kinesis is being held.
    /// </summary>
    public void UpdateKinesis()
    {
        radialScript.SelectKinesis();
    }

    /// <summary>
    /// Returns the radial menu script.
    /// </summary>
    /// <returns></returns>
    public RadialMenu GetRadialScript()
    {
        return radialScript;
    }

    /// <summary>
    /// Shows the info menu for item pickups.
    /// </summary>
    /// <param name="img">The image of the item.</param>
    /// <param name="name">The name of the item.</param>
    public void ShowInfoUI(Sprite img, string name)
    {
        GameManager.instance.TimePause();
        objToShow.SetActive(true);
        textToReplace.text = name;
        displayedImage.sprite = img;
    }

    /// <summary>
    /// Closes the menu for item pickups.
    /// </summary>
    public void CloseInfoUI()
    {
        GameManager.instance.TimeUnpause();
        objToShow.SetActive(false);
        textToReplace.text = "You did something wrong?";
        displayedImage.sprite = null;
    }

    /// <summary>
    /// Runs the saving icon at the bottom right of the screen
    /// </summary>
    public void SaveIcon()
    {
        savingIcon.SetActive(true);
        loadingIconScript.WakeUp();
    }

    /// <summary>
    /// Closes the death menu UI when the player respawns.
    /// </summary>
    public void CloseDeathUI()
    {
        loseAnimController.SetTrigger("ExitLose");
        //Debug.LogError("Running Exit Lose Menu Close");
    }
}
