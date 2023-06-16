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
    [SerializeField] GameObject settMenuObj;

    // Start is called before the first frame update
    void Start()
    {
        baseMenu.SetActive(true);
        settMenuObj.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void NewGame()
    {
        eScript.FadeTo("Tom_Tutorial_Shooting_Range");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SettingsMM()
    {
        baseMenu.SetActive(false);
        settMenuObj.SetActive(true);
    }

    public void ApplySettingsMM()
    {
        sett.SaveToSettingsObj();
        settMenuObj.SetActive(false);
        baseMenu.SetActive(true);
    }

    public void CancelSettignsMM()
    {
        sett.CancelSettingsObj();
        settMenuObj.SetActive(false);
        baseMenu.SetActive(true);
    }
}
