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
        Debug.Log("Main Menu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
        gameManager.instance.Unpaused();
        //May change function later
        // SceneManager.LoadScene(SceneManager.GetSceneByName();
    }
}
