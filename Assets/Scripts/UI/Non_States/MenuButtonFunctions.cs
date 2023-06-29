using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonFunctions : MonoBehaviour
{
    public void Resume()
    {
        UIManager.instance.uiStateMachine.SetOnEscape(true);
    }
    public void Restart()
    {
        UIManager.instance.Unpaused();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitToMenu()
    {
        UIManager.instance.Unpaused();
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
        UIManager.instance.Unpaused();
        // Will need to implement a way to get the next level's name
        // from the game manager and load the scene via that.
        // This script should be solely for functions and using them.
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
