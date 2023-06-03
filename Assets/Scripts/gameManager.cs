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
        //pauses the game
        if (Input.GetKeyDown(KeyCode.Escape) && activeMenu == null)
        {
           
            activeMenu = pausemenu;
            activeMenu.SetActive(true);
            Paused();
        
        }
    }
    //stes game to paused state
    public void Paused()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        UI_Manager.instance.EnableBoolAnimator(UI_Manager.instance.PausePanel);
    }
    
    //resumes game while paused
    public void Unpaused()
    {
        UI_Manager.instance.DisableBoolAnimator(UI_Manager.instance.PausePanel);
        Time.timeScale = timescaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
       
        activeMenu.SetActive(false);
        activeMenu = null;
     
    }

    //function for when the game is won
    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(3);
        activeMenu = winMenu;
        activeMenu.SetActive(true);
        Paused();
    }
    //function for when the game is lost
    IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(3);
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        Paused();
    }


}
