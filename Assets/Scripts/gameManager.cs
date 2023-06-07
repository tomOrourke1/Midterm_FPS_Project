using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("-----Player Stuff-----")]
    public GameObject player;
    public Player playerscript;

    [Header("-----UI Stuff-----")]
    public GameObject pausemenu;
    public GameObject activeMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject PlayerSpawnPOS;
  
    private RadialMenu radialMenuScriptRef;
    public PlayerStats_UI pStatsUI;
    [SerializeField] GameObject flashDamage;
    float timescaleOrig;

    private int enemiesRemaining;
    public int KeyCounter;
    public TextMeshProUGUI enemiesRemainingText;

    void Awake()
    {
        instance = this;
        timescaleOrig = Time.timeScale;

        //sets both the player and player script
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent<Player>();
        //sets spawn point
        PlayerSpawnPOS = GameObject.FindGameObjectWithTag("Player Spawn Pos");
        // 6/5/2023 - Kevin W.
        // Gets the Radial Menu Script off of the UI Game Object
        radialMenuScriptRef = GetComponent<RadialMenu>();
        pStatsUI = GetComponent<PlayerStats_UI>();
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
            UI_Manager.instance.EnableBoolAnimator(UI_Manager.instance.PausePanel);
        }
 
        // 6/5/2023 - Kevin W.
        // Updates the keys in the Radial Menu Script
        radialMenuScriptRef.UpdateKeys();
        // remove this when taking damage and receiving damage is implemented
        // and replace it to update the corresponding damage of the type (HP | Focus | Shield)
        // when those types are taken. 
        pStatsUI.UpdateValues();
    }
    //stes game to paused state
    public void Paused()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    //resumes game while paused
    public void Unpaused()
    {
        UI_Manager.instance.DisableBoolAnimator(UI_Manager.instance.PausePanel);
        UI_Manager.instance.DisableBoolAnimator(UI_Manager.instance.WinPanel);
        UI_Manager.instance.DisableBoolAnimator(UI_Manager.instance.LossPanel);
        Time.timeScale = timescaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        StartCoroutine(WaitToTurnOffUI());
    }

    IEnumerator WaitToTurnOffUI()
    {
        yield return new WaitForSeconds(0.5f);
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
        UI_Manager.instance.EnableBoolAnimator(UI_Manager.instance.WinPanel);
    }
    //function for when the game is lost
    public IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(3);
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        Paused();
        UI_Manager.instance.EnableBoolAnimator(UI_Manager.instance.LossPanel);
    }
    public void UpdateGameGoal(int amount)
    {
        enemiesRemaining += amount;
        enemiesRemainingText.text = enemiesRemaining.ToString("F0");
        if(enemiesRemaining <= 0)
        {
            StartCoroutine(WinGame());
        }
    }
    public int GetKeyCounter() 
    {
        return KeyCounter;
    }
    public void SetKeyCounter(int counter) 
    {
     KeyCounter = counter;
    }
}
