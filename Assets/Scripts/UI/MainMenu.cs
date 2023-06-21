using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] SettingsManager sett;
    [SerializeField] GameObject settingsMenuObj;
    [SerializeField] GameObject keybinds;

    [SerializeField] string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        baseMenu.SetActive(true);
        settingsMenuObj.SetActive(false);
        keybinds.SetActive(false);

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

    public void SettingsMM()
    {
        baseMenu.SetActive(false);
        settingsMenuObj.SetActive(true);
    }

    public void ApplySettingsMM()
    {
        sett.SaveToSettingsObj();
        settingsMenuObj.SetActive(false);
        baseMenu.SetActive(true);
    }

    public void CancelSettignsMM()
    {
        sett.CancelSettingsObj();
        settingsMenuObj.SetActive(false);
        baseMenu.SetActive(true);
    }

    public void OpenKeybinds()
    {
        baseMenu.SetActive(false);
        keybinds.SetActive(true);
    }

    public void HideKeybinds()
    {
        baseMenu.SetActive(true);
        keybinds.SetActive(false);
    }
}
