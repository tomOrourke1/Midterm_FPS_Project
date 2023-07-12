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

    [SerializeField] Image sceneFader;
    [SerializeField] ElevatorScript eScript;
    [SerializeField] GameObject baseMenu;

    [SerializeField] GameObject settingsSelectedFirst;
    [SerializeField] GameObject mainMenuFirstSelected;

    [SerializeField] MainMenuConfirmUI mmcu;

    [Header("Settings Components")]
    [SerializeField] GameObject settingsMenuObj;

    [Header("Audio Mixer")]
    [SerializeField] AudioManagerMainMenu mainMenusAudio;

    [Header("Scene Loading Info")]
    [SerializeField] string tutorialLevelStart;
    [SerializeField] TextMeshProUGUI gameContinueText;
    private string sceneToLoad;

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

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    public void NewGame()
    {
        eScript.FadeTo(sceneToLoad);
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


    public void ShowSettingsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsSelectedFirst);

        baseMenu.SetActive(false);
        settingsMenuObj.SetActive(true);
        mainMenusAudio?.RunMuffler();
    }

    public void CloseSettingsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);

        settingsMenuObj.SetActive(false);
        baseMenu.SetActive(true);
        mainMenusAudio?.RunUnMuffler();
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
            CloseSettingsMenu();
        }
        else if (!settingsMenuObj.activeInHierarchy)
        {
            ShowSettingsMenu();
        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected);
    }
}
