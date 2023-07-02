using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Image sceneFader;
    [SerializeField] ElevatorScript eScript;
    [SerializeField] GameObject baseMenu;

    [Header("Settings Components")]
    [SerializeField] GameObject settingsMenuObj;

    [Header("Scene Loading Info")]
    [SerializeField] string tutorialLevelStart;
    [SerializeField] TextMeshProUGUI gameContinueText;
    string sceneToLoad;


    // Start is called before the first frame update
    void Start()
    {
        GameLoadData();
        SetGameText();

        baseMenu.SetActive(true);
        settingsMenuObj?.SetActive(false);

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

    public void ShowSettingsMenu()
    {
        baseMenu.SetActive(false);
        settingsMenuObj.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        settingsMenuObj.SetActive(false);
        baseMenu.SetActive(true);
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
}
