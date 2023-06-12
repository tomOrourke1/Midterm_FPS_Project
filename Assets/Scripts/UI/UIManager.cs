using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
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
    [SerializeField] Image sceneFader;

    [Header("Hitmarker")]
    [SerializeField] GameObject hitmarker;
    
    [Header("Stats UI")]
    [SerializeField] PlayerStatsUI statsUIRef;
    [SerializeField] GameObject playerStatsObj;

    [Header("Menu States")]
    [SerializeField] GameObject activeMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject radialMenu;

    [Header("Animator Components")]
    [SerializeField] Animator PauseAnimController;
    [SerializeField] Animator WinAnimControlller;
    [SerializeField] Animator LoseAnimController;

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

    private void Update()
    {
        //pauses the game
        if (Input.GetKeyDown(KeyCode.Escape) && activeMenu == null && !Input.GetKeyUp(KeyCode.Q) && menuState != MenuState.active)
        {
            activeMenu = pauseMenu;
            activeMenu.SetActive(true);
            PauseGame();
            //EnableBoolAnimator(PausePanel);
        }
        else if (activeMenu == null && menuState != MenuState.none)
        {
            //radialMenuScriptRef.UpdateKeys();
        }

    }

    // Sets game to paused state
    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        playerStatsObj.SetActive(false);
        menuState = MenuState.none;
    }

    // resumes game while paused
    public void Unpaused()
    {
        PauseAnimController.SetTrigger("ExitPause");
        LoseAnimController.SetTrigger("ExitLose");
        WinAnimControlller.SetTrigger("ExitWin");

        Time.timeScale = GameManager.instance.GetOriginalTimeScale();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerStatsObj.SetActive(true);

        StartCoroutine(WaitToTurnOffUI());
    }

    public void SettingsShown()
    {
        activeMenu.SetActive(false);
        activeMenu = settingsMenu;
        activeMenu.SetActive(true);
    }

    public void ApplySettings()
    {
        activeMenu.SetActive(false);
        activeMenu = pauseMenu;
        activeMenu.SetActive(true);
    }

    IEnumerator WaitToTurnOffUI()
    {
        yield return new WaitForSeconds(0.5f);
        activeMenu.SetActive(false);
        activeMenu = null;

        menuState = MenuState.radial;
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(3);
        activeMenu = winMenu;
        activeMenu.SetActive(true);
        PauseGame();
    }

    public void LoseGame()
    {
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        PauseGame();
    }

    private void DisableMenus()
    {
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        radialMenu.SetActive(false);
        hitmarker.SetActive(false);
    }
    public PlayerStatsUI GetPlayerStats()
    {
        return statsUIRef;
    }

    public Image GetSceneFader()
    {
        return sceneFader;
    }

    public FlashDamage GetFlashDamageScript()
    {
        return flashImageScript;
    }

    public void FlashBreakShield()
    {
        StartCoroutine(flashImageScript.CrackShieldDisplay());
    }

    public void FlashPlayerHealthHit()
    {
        StartCoroutine(flashImageScript.FlashDamageDisplay());
    }
    public void FlashPlayerShieldHit()
    {
        StartCoroutine(flashImageScript.FlashShieldDisplay());
    }

    public GameObject GetHitmarker()
    {
        return hitmarker;
    }
}
