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
    private string sceneToLoad;

    [Header("Event System")]
    [SerializeField] GameObject settingsSelectedFirst;
    [SerializeField] GameObject mainMenuFirstSelected;
    [SerializeField] GameObject levelSelectFirstSelected;

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
        InputManager.Instance.Input.Enable();
        GameLoadData();
        SetGameText();

        baseMenu.SetActive(true);
        settingsMenuObj?.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);

        StartCoroutine(CursorMainMenuFix());
    }

    private IEnumerator CursorMainMenuFix()
    {
        yield return new WaitForEndOfFrame();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void NewGame()
    {
        eScript.FadeTo(GameManager.instance.GetNextLevel());
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
        mainButtonsObj.SetActive(false);
        levelSelectObj.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelSelectFirstSelected);
    }

    public void LevelSelection(int pos)
    {
        lSScript.Selection(pos);
    }


    public void ShowSettingsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsSelectedFirst);

        baseMenu.SetActive(false);
        settingsMenuObj.SetActive(true);
        mainMenusAudio?.RunMuffler();
    }

    public void CloseMenus()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);

        settingsMenuObj.SetActive(false);
        levelSelectObj.SetActive(false);
        baseMenu.SetActive(true);
        mainButtonsObj.SetActive(true);
        mainMenusAudio?.RunUnMuffler();
        StartCoroutine(CursorMainMenuFix());
    }

    /// <summary>
    /// Processess if the player is starting a new game or continuing off of the old one.
    /// </summary>
    private void GameLoadData()
    {
        sceneToLoad = GameManager.instance.GetNextLevel();
    }

    private void SetGameText()
    {
        if (tutorialLevelStart == GameManager.instance.GetSettingsManager().settings.currentScene)
        {
            gameContinueText.text = "New Game";
        }
        else
        {
            gameContinueText.text = "Continue";
        }
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
}
