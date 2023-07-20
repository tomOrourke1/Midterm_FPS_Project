using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentSelectedFix : MonoBehaviour
{
    [Header("Death Fix")]
    [SerializeField] DeathActivation deathActivateEvent;
    [SerializeField] GameObject deathObj;
    [Header("Info Fix")]
    [SerializeField] InfographicActivation infoActivateEvent;
    [SerializeField] GameObject infoObj;
    [Header("Pause Fix")]
    [SerializeField] PauseActivation pauseActivateEvent;
    [SerializeField] GameObject pauseObj;
    [Header("Play Fix")]
    [SerializeField] PlayActivation playActivateEvent;
    [SerializeField] GameObject playObj;
    [Header("Settings Fix")]
    [SerializeField] SettingsActivation settingsActivateEvent;
    [SerializeField] GameObject settingsObj;
    [Header("DisplaySettings Fix")]
    [SerializeField] DisplaySettingsBoxes settingsMenuSelectedBoxEvent;
    [SerializeField] GameObject displaySettingsObj;
    [Header("MainMenu Fix")]
    [SerializeField] MainMenu mainMenuActivateEvent;
    [SerializeField] GameObject mainMenuObj;
    [Header("ConfirmUI Fix")]
    [SerializeField] MainMenuConfirmUI confirmUIEvent;
    [SerializeField] GameObject confirmUI;

    public void RunPing()
    {
        // run PingCurrentEvent on each script after checking if the object is active
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            RunPing_OnMainMenuObjs();
        }
        else
        {
            RunPing_OnNonMainMenuObjs();
        }
    }

    private void RunPing_OnMainMenuObjs()
    {
        if (deathObj.activeInHierarchy)
        {

        }
        else if (pauseObj.activeInHierarchy)
        {

        }
        else if (infoObj.activeInHierarchy)
        {

        }
        else if (playObj.activeInHierarchy)
        {

        }
        
    }

    private void RunPing_OnNonMainMenuObjs()
    {

    }
}
