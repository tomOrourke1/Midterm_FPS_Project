using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButtonFunctions : MonoBehaviour
{
    public void Resume()
    {
        UIManager.instance.uiStateMachine.SetOnEscape(true);
    }

    public void Restart()
    {
        GameManager.instance.PlayMenuState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToConfirm()
    {
        UIManager.instance.ShowConfirmMainMenu();
    }

    public void ConfirmToMainMenu()
    {
        GameManager.instance.AudioManagerUnmuffle();
        UIManager.instance.TurnOffCameraScript();
        GameManager.instance.GetPlayerResources().SetVulnerability(false);        
        UIManager.instance.GetElevatorScript().FadeTo("MainMenu");
    }

    public void ConfirmToPause()
    {
        UIManager.instance.CloseMainMenu();
    }
    
    public void NextLevel()
    {
        GameManager.instance.PlayMenuState();
    }

    public void Respawn()
    {
        GameManager.instance.RespawnCaller();
        UIManager.instance.uiStateMachine.SetDeathContinueBool(true);
    }

    public void ShowSettingsMenu()
    {
        UIManager.instance.uiStateMachine.SetSettingsAsync(true);
    }

}
