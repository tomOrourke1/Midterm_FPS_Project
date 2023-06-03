using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public static gameManager instance;

    [Header("-----Player Stuff-----")]
    public GameObject player;
    //public INSERT_PLAYERSCRIPT_HERE playerscript;

    [Header("-----UI Stuff-----")]
    public GameObject pausemenu;
    public GameObject activeMenu;
    public GameObject winMenu;
    public GameObject loseMenu;

    public bool isPaused;
    float timescaleOrig;
  
    void Awake()
    {
        instance = this;
        timescaleOrig = Time.timeScale;
       // player = GameObject.FindGameObjectWithTag("INSERT_PLAYER_HERE");
        //playerscript = player.GetComponent<INSERT_PLAYER_SCRIPT_HERE>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && activeMenu == null)
        {
            isPaused = !isPaused;
            activeMenu = pausemenu;
            activeMenu.SetActive(isPaused);
            Paused();
        }
    }
    public void Paused()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Unpaused()
    {
        Time.timeScale = timescaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused =!isPaused;
        activeMenu.SetActive(false);
        activeMenu = null;
    }
    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(3);
        activeMenu = winMenu;
        activeMenu.SetActive(true);
        Paused();
    }
    IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(3);
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        Paused();
    }


}
