using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonFunctions : MonoBehaviour
{

    // Start is called before the first frame update
    public void Resume()
    {
        UIManager.instance.Unpaused();
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
        UIManager.instance.CloseDeathUI();
        UIManager.instance.uiStateMachine.SetPlay(true);
        UIManager.instance.Unpaused();
        GameManager.instance.RespawnCaller();

    }

    public void ShowKeybindsMenu()
    {
        //UIManager.instance.ShowKeybinds();
    }

    public void ShowCheatMenu()
    {
        //UIManager.instance.RunCheatMenu();
    }
}
