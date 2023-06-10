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

    // Start is called before the first frame update
    void Start()
    {
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
}
