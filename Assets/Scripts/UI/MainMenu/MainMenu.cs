using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    [Header("Scene Transitioning")]
    [SerializeField] Image sceneFader;
    [SerializeField] ElevatorScript eScript;
    [SerializeField] GameObject baseMenu;
    [SerializeField] string tutorialLevelStart;
    [SerializeField] TextMeshProUGUI gameContinueText;

    [Header("Event System")]
    [SerializeField] GameObject settingsSelectedFirst;
    [SerializeField] GameObject mainMenuFirstSelected;
    [SerializeField] GameObject levelSelectFirstSelected;
    [SerializeField] GameObject pingFix_Base;
    [SerializeField] GameObject pingFix_Confirm;

    [Header("Confirm Close Button")]
    [SerializeField] MainMenuConfirmUI mmcu;

    [Header("Settings Components")]
    [SerializeField] GameObject settingsMenuObj;

    [Header("Audio Mixer")]
    [SerializeField] AudioManagerMainMenu mainMenusAudio;

    LevelSelectScript lSScript;
    [SerializeField] GameObject levelSelectObj;
    [SerializeField] GameObject mainButtonsObj;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Just dont bother arguing with me, im trying to fix the mouse not working at this point. IM LOSING MY MIND!
        Time.timeScale = GameManager.instance.GetSettingsManager().GetOriginalTimeScale();

        InputManager.Instance.Input.Enable();

        baseMenu.SetActive(true);
        settingsMenuObj?.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        PingEventSystem_FixPlay();
    }


    public void NewGame()
    {
        eScript.FadeTo("Tom_Tutorial_MK_2");
    }

    public void ContinueGame()
    {
        eScript.FadeTo(GameManager.instance.GetSettingsManager().settings.currentScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Runs the Main Menu Confirm UI Script 
    /// </summary>
    public void ShowConfirmMainMenu()
    {
        mmcu.OpenConfirm();
    }

    /// <summary>
    /// Runs the Main Menu UI Close UI
    /// </summary>
    public void CloseMainMenu()
    {
        mmcu.CloseConfirm();
    }

    public void ToggleLevelSelectionMenu()
    {
        if (!levelSelectObj.activeInHierarchy)
        {
            ShowLevelSelect();
        }
        else if (levelSelectObj.activeInHierarchy)
        {
            CloseMenus();
        }
    }

    private void ShowLevelSelect()
    {
        PingEventSystem_FixLevelSelect();
        
        mainButtonsObj.SetActive(false);
        levelSelectObj.SetActive(true);
    }

    public void LevelSelection(int pos)
    {
        lSScript.Selection(pos);
    }


    public void ShowSettingsMenu()
    {
        PingEventSystem_FixShowSettings();
        
        baseMenu.SetActive(false);
        settingsMenuObj.SetActive(true);
        mainMenusAudio?.RunMuffler();
    }

    public void CloseMenus()
    {
        PingEventSystem_FixPlay();

        settingsMenuObj.SetActive(false);
        levelSelectObj.SetActive(false);
        baseMenu.SetActive(true);
        mainButtonsObj.SetActive(true);
        mainMenusAudio?.RunUnMuffler();
    }

    public void ToggleSettings()
    {
        if (settingsMenuObj.activeInHierarchy)
        {
            CloseMenus();
        }
        else if (!settingsMenuObj.activeInHierarchy)
        {
            ShowSettingsMenu();
        }
    }

    public void Ping_FixEventSystem()
    {
        if (pingFix_Confirm.activeInHierarchy)
        {
            mmcu.RunPingEvent();
        }
        else if (pingFix_Base.activeInHierarchy)
        {
            PingEventSystem_FixPlay();
        }
        else if (levelSelectObj.activeInHierarchy)
        {
            PingEventSystem_FixLevelSelect();
        }
    }


    private void PingEventSystem_FixPlay()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);
    }

    private void PingEventSystem_FixLevelSelect()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelSelectFirstSelected);
    }

    private void PingEventSystem_FixShowSettings()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsSelectedFirst);
    }
}
