using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum MenuState
{
    radial,
    active,
    none
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public MenuState menuState;
    private FlashDamage flashImageScript;

    [Header("Radial Menu Script")]
    [SerializeField] RadialMenu radialScript;

    [Header("Menu States")]
    [SerializeField] GameObject activeMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject radialMenu;

    [Header("Stats UI")]
    [SerializeField] PlayerStatsUI statsUIRef;
    [SerializeField] GameObject playerStatsObj;

    [Header("Animator Components")]
    [SerializeField] Animator PauseAnimController;
    [SerializeField] Animator WinAnimControlller;
    [SerializeField] Animator LoseAnimController;
    
    [Header("Misc Images")]
    [SerializeField] GameObject hitmarker;
    [SerializeField] Image sceneFader;

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
        activeMenu = pauseMenu;
        activeMenu.SetActive(true);
        GameManager.instance.TimePause();
        GameManager.instance.MouseUnlockShow();
        playerStatsObj.SetActive(false);
        menuState = MenuState.none;
    }

    /// <summary>
    /// Unpauses the game. Resets the cursor back to being invisible and locked.
    /// </summary>
    public void Unpaused()
    {
        PauseAnimController.SetTrigger("ExitPause");
        LoseAnimController.SetTrigger("ExitLose");
        WinAnimControlller.SetTrigger("ExitWin");

        GameManager.instance.TimeUnpause();
        GameManager.instance.MouseLockHide();
        playerStatsObj.SetActive(true);


        // See if we can fix this glitch.
        StartCoroutine(WaitToTurnOffUI());
    }

    /// <summary>
    /// Runs the ShowRadialMenu function from the radial menu script.
    /// </summary>
    public void ShowRadialMenu()
    {
        radialScript.ShowRadialMenu();
    }

    /// <summary>
    /// Runs the HideRadialMenu function from the radial menu script.
    /// </summary>
    public void HideRadialMenu()
    {
        radialScript.HideRadialMenu();
    }

    /// <summary>
    /// Runs the SelectSlice function from the radial menu script.
    /// </summary>
    public void UpdateRadialWheel()
    {
        radialScript.SelectSlice();
    }

    /// <summary>
    /// Shows the settings menu. Should not be called anywhere in code. 
    /// Should only be used in the settings button under the pause menu settings option button.
    /// </summary>
    public void SettingsShown()
    {
        activeMenu.SetActive(false);

        activeMenu = settingsMenu;
        activeMenu.SetActive(true);
    }
    
    /// <summary>
    /// Closes the settings menu and shows the pause menu. Also sets the ActiveMenu to the pause menu.
    /// </summary>
    public void ApplySettings()
    {
        activeMenu.SetActive(false);
        activeMenu = pauseMenu;
        activeMenu.SetActive(true);
    }

    /// <summary>
    /// I dont know. This is apparently here to fix a bug, but might not be needed. Will need to test that.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitToTurnOffUI()
    {
        yield return new WaitForSeconds(0.5f);
        activeMenu.SetActive(false);
        activeMenu = null;

        menuState = MenuState.radial;
    }

    /// <summary>
    /// Displays the win game UI. Don't totally need this but we can repurpose this for the end credits later.
    /// </summary>
    /// <returns></returns>
    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(3);
        activeMenu = winMenu;
        activeMenu.SetActive(true);
        PauseGame();
    }

    /// <summary>
    /// Runs the lose game logic, also sets the active menu to the lose menu.
    /// Also pauses the game.
    /// </summary>
    public void LoseGame()
    {
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        PauseGame();
    }

    /// <summary>
    /// Sets the | Pause Menu, Win Menu, Lose Menu, Settings Menu, Radial Menu, and Hitmarker (image) |
    /// to false when loading the game initially. Should only be called when the game starts in the UI manager.
    /// </summary>
    private void DisableMenus()
    {
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        radialMenu.SetActive(false);
        hitmarker.SetActive(false);
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
}
