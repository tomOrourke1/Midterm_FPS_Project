using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] LoadingIcon loadingIconScript;
    [SerializeField] DamageIndicator damageIndicatorScript;

    [Header("Stats UI")]
    [SerializeField] GameObject playerStatsObj;

    [Header("Confirm Menu")]
    [SerializeField] ElevatorScript gameManagerESCRIPT;
    [SerializeField] MainMenuConfirmUI confirmUIScript;

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
    [SerializeField] FocusDepleteBarUpdate depleteScript;

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
    /// Runs the lose game logic, also sets the active menu to the lose menu.
    /// Also pauses the game.
    /// </summary>
    public void LoseGame()
    {
        GameManager.instance.TimePause();
        GameManager.instance.MouseUnlockShow();
    }

    /// <summary>
    /// Runs the SelectSlice function from the radial menu script.
    /// </summary>
    public void UpdateRadialWheel()
    {
        radialScript.SelectSlice();
    }

    #region Camera Enable/Disable
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
    #endregion

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

    #region FlashDamage Image StartCoroutines
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
    #endregion
    
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
    /// Runs the saving icon at the bottom right of the screen
    /// </summary>
    public void SaveIcon()
    {
        savingIcon.SetActive(true);
        loadingIconScript.WakeUp();
    }

    /// <summary>
    /// Flashs the depleted focus bar.
    /// </summary>
    public void FocusDepleted()
    {
        depleteScript.ShowDeplete();
    }
    
    /// <summary>
    /// Flashes the screen on low HP.
    /// </summary>
    public void FlashLOWHP()
    {
        flashImageScript.RunHPFlash();
    }

    /// <summary>
    /// Stops flashing the screen on low HP.
    /// </summary>
    public void StopFlashLowHP()
    {
        flashImageScript.StopHPFlasher();
    }

    #region ExitToMainMenu functions
    /// <summary>
    /// Runs the Main Menu Confirm UI Script 
    /// </summary>
    public void ShowConfirmMainMenu()
    {
        confirmUIScript.OpenConfirm();
    }

    /// <summary>
    /// Runs the Main Menu UI Close UI
    /// </summary>
    public void CloseMainMenu()
    {
        confirmUIScript.CloseConfirm();
    }
    #endregion

    public IEnumerator WaitCloseConfirm()
    {
        yield return new WaitForSeconds(0.25f);
        CloseMainMenu();
    }

    #region Script References
    // ==============================================
    //      Getters for script references
    // ==============================================

    /// <summary>
    /// Returns the radial menu script.
    /// </summary>
    /// <returns></returns>
    public RadialMenu GetRadialScript()
    {
        return radialScript;
    }
    
    /// <summary>
    /// Script that displays the latest hitpoint to the player.
    /// </summary>
    /// <returns>DamageIndicator Script</returns>
    public DamageIndicator GetDamageIndicatorScript()
    {
        return damageIndicatorScript;
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
    /// Gets the Key UI Script.
    /// </summary>
    /// <returns></returns>
    public KeyUI GetKeyUI()
    {
        return keyScriptRef;
    }

    /// <summary>
    /// Returns the Elevator Script from the UI Mananger.
    /// </summary>
    /// <returns></returns>
    public ElevatorScript GetElevatorScript()
    {
        return gameManagerESCRIPT;
    }

    public void DeathEvents()
    {
        StopFlashLowHP();
        damageIndicatorScript.Disabler();
        flashImageScript.Disabler();
    }
    #endregion
}
