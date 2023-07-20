using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSelectedFix : MonoBehaviour
{
    [SerializeField] DeathActivation deathActivateEvent;
    [SerializeField] InfographicActivation infoActivateEvent;
    [SerializeField] PauseActivation pauseActivateEvent;
    [SerializeField] PlayActivation playActivateEvent;
    [SerializeField] SettingsActivation settingsActivateEvent;
    [SerializeField] DisplaySettingsBoxes settingsMenuSelectedBoxEvent;
    [SerializeField] SettingsManager settingsManagerActivateEvent;
    [SerializeField] MainMenu mainMenuActivateEvent;
    [SerializeField] MainMenuConfirmUI confirmUIEvent;

    public void RunPing()
    {
        // run PingCurrentEvent on each script after checking if the object is active
    }





}
