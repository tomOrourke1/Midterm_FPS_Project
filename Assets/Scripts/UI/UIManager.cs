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
    paused,
    death,
    none
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public MenuState currentState;
    private FlashDamage flashImageScript;

    [Header("Script Reference")]
    [SerializeField] RadialMenu radialScript;
    [SerializeField] PlayerStatsUI statsUIRef;
    [SerializeField] KeyUI keyScriptRef;

    [Header("Menu States")]
    [SerializeField] GameObject activeMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject radialMenu;

    [Header("Stats UI")]
    [SerializeField] GameObject playerStatsObj;

    [Header("Animator Components")]
    [SerializeField] Animator pauseAnimController;
    [SerializeField] Animator winAnimController;
    [SerializeField] Animator loseAnimController;
    
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
        currentState = MenuState.paused;
        activeMenu = pauseMenu;
        activeMenu.SetActive(true);
        GameManager.instance.TimePause();
        GameManager.instance.MouseUnlockShow();
        playerStatsObj.SetActive(false);
        pauseAnimController.SetBool("ExitPause", false);
    }

    /// <summary>
    /// Unpauses the game. Resets the cursor back to being invisible and locked.
    /// </summary>
    public void Unpaused()
    {
        if (currentState == MenuState.paused)
        {
            pauseAnimController.SetBool("ExitPause", true);
        }
        else if (currentState == MenuState.death)
        {
            loseAnimController.SetTrigger("ExitLose");
            DisableMenus();
        }
        GameManager.instance.TimeUnpause();
        GameManager.instance.MouseLockHide();
        playerStatsObj.SetActive(true);

        currentState = MenuState.none;
    }

    /// <summary>
    /// Runs the ShowRadialMenu function from the radial menu script.
    /// </summary>
    public void ShowRadialMenu()
    {
        currentState = MenuState.radial;
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
        currentState = MenuState.none;
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
        currentState = MenuState.death;
        activeMenu.SetActive(true);
        GameManager.instance.TimePause();
        GameManager.instance.MouseUnlockShow();
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

    public KeyUI GetKeyUI()
    {
        return keyScriptRef;
    }
}
