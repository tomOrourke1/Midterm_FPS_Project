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

    [Header("Stats UI Script")]
    [SerializeField] PlayerStatsUI statsUIRef;

    [Header("Menu States ")]
    [SerializeField] GameObject activeMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject radialMenu;

    [Header("Animator Components")]
    [SerializeField] Animator PausePanel;
    [SerializeField] Animator WinPanel;
    [SerializeField] Animator LossPanel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
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
            Paused();
            EnableBoolAnimator(PausePanel);
        }
        else if (activeMenu == null && menuState != MenuState.none)
        {
            //radialMenuScriptRef.UpdateKeys();
        }

    }

    // Sets game to paused state
    public void Paused()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        menuState = MenuState.none;
    }

    // resumes game while paused
    public void Unpaused()
    {
        DisableBoolAnimator(PausePanel);
        DisableBoolAnimator(WinPanel);
        DisableBoolAnimator(LossPanel);
        Time.timeScale = GameManager.instance.GetOriginalTimeScale();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine(WaitToTurnOffUI());
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
        Paused();
        EnableBoolAnimator(WinPanel);
    }

    public void LoseGame()
    {
        Paused();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        EnableBoolAnimator(LossPanel);
    }

    private void DisableMenus()
    {
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
    }

   
    //Function will remove the pause menu from the screen
    public void DisableBoolAnimator(Animator animator)
    {
        animator.SetBool("IsDisplayed", false);
    }

    //Will bring the pause menu on screen
    public void EnableBoolAnimator(Animator animator)
    {
        animator.SetBool("IsDisplayed", true);
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
}
