using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonFunctions : MonoBehaviour
{

    // Start is called before the first frame update
    public void Resume()
    {
        gameManager.instance.Unpaused();
    }
    public void Restart()
    {
        gameManager.instance.Unpaused();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitToMenu()
    {
        gameManager.instance.Unpaused();
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
        gameManager.instance.Unpaused();
        // Will need to implement a way to get the next level's name
        // from the game manager and load the scene via that.
        // This script should be solely for functions and using them.
    }
    public void Respawn()
    {
        gameManager.instance.Unpaused();
        gameManager.instance.playerscript.RespawnPlayer();
    }

}
