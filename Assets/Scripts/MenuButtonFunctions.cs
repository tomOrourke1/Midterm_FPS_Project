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
    }
    public void ExitToMenu()
    {
        Debug.Log("Main Menu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
