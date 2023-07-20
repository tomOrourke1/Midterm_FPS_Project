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
    [Header("ConfirmUI Fix")]
    [SerializeField] MainMenuConfirmUI confirmUIEvent;
    [SerializeField] GameObject confirmUI;

    public void RunPing()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            RunPing_Normal();
        }
    }

    private void RunPing_Normal()
    {
        if (deathObj.activeInHierarchy)
        {
            deathActivateEvent.PingCurrentEvent();
        }
        else if (pauseObj.activeInHierarchy)
        {
            pauseActivateEvent.PingCurrentEvent();
        }
        else if (infoObj.activeInHierarchy)
        {
            infoActivateEvent.PingCurrentEvent();
        }
        else if (playObj.activeInHierarchy)
        {
            pauseActivateEvent.PingCurrentEvent();
        }
        else if (settingsObj.activeInHierarchy)
        {
            settingsActivateEvent.PingCurrentEvent();
        }
    }
}
