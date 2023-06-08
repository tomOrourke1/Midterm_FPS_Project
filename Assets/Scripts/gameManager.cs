using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel;
using UnityEngine.XR;
using UnityEngine.UI;
using Unity.VisualScripting;

public enum MenuState
{
    radial,
    active,
    none
}

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("-----Player Stuff-----")]
    public GameObject player;
    public Player playerscript;
    public PlayerStats_UI pStatsUI;
    [SerializeField] GameObject flashDamage;

    [Header(" Menu States ")]
    [SerializeField] GameObject pausemenu;
    [SerializeField] GameObject activeMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject radialMenu;
    [SerializeField] GameObject PlayerSpawnPOS;

    [Header("Objective Items")]
    public int KeyCounter;
    public TextMeshProUGUI enemiesRemainingText;
    [SerializeField] float objectiveFadeInTimer;
    [SerializeField] AnimationCurve displayCurve;
    [SerializeField] GameObject firstObjective;
    [SerializeField] GameObject elevatorObjectiveDisplay;
    private GameObject fadeInObjective;

    private RadialMenu radialMenuScriptRef;
    private float timescaleOrig;
    private int enemiesRemaining;

    public MenuState menuState;

    void Awake()
    {
        instance = this;
        timescaleOrig = Time.timeScale;

        //sets both the player and player script
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent<Player>();
        //sets spawn point
        PlayerSpawnPOS = GameObject.FindGameObjectWithTag("Player Spawn Pos");
        
        radialMenuScriptRef = GetComponent<RadialMenu>();
        pStatsUI = GetComponent<PlayerStats_UI>();

        elevatorObjectiveDisplay.SetActive(false);
        firstObjective.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        //pauses the game
        if (Input.GetKeyDown(KeyCode.Escape) && activeMenu == null && !Input.GetKeyUp(KeyCode.Q) && menuState != MenuState.active)
        {
            activeMenu = pausemenu;
            activeMenu.SetActive(true);
            Paused();
            UI_Manager.instance.EnableBoolAnimator(UI_Manager.instance.PausePanel);
        }
        else if (activeMenu == null && menuState != MenuState.none) 
        {
            //radialMenuScriptRef.UpdateKeys();
            
        }

        // testing switching objectives - kevin w.
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartObjective();
        }
    }
    //stes game to paused state
    public void Paused()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;


        menuState = MenuState.none;
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

        menuState = MenuState.radial;
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
    public void LoseGame()
    {
        Paused();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        UI_Manager.instance.EnableBoolAnimator(UI_Manager.instance.LossPanel);
    }
    public void UpdateGameGoal(int amount)
    {
        enemiesRemaining += amount;
        enemiesRemainingText.text = enemiesRemaining.ToString("F0");
        if(enemiesRemaining <= 0)
        {
            DisplayElevatorObjective();
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
    private void DisableMenus() 
    {
        pausemenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
    }

    public Image GetFlashImage()
    {
        return flashDamage.GetComponent<Image>();
    }

    public GameObject GetPlayerSpawnPOS()
    {
        return PlayerSpawnPOS;
    }

    public void SetPlayerSpawnPos(GameObject _spawnPosGameObj)
    {
        PlayerSpawnPOS = _spawnPosGameObj;
    }

    private void DisplayElevatorObjective()
    {
        elevatorObjectiveDisplay.SetActive(true);
        firstObjective.SetActive(false);
    }

    private void StartObjective()
    {
        fadeInObjective = firstObjective;
        StartCoroutine(MoveTransitionIn());
        // Going to add more stuff about keeping track of objectives and such.
    }

    IEnumerator MoveTransitionIn()
    {
        // Create a float storing the timer
        float timerFadeIn = 0f;

        // While the timer is above a 'second'
        while (timerFadeIn > objectiveFadeInTimer)
        {
            // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
            timerFadeIn += Time.deltaTime;
            // Evaluate the curve and then set that the movement amount
            float moveAmt = displayCurve.Evaluate(timerFadeIn);
            fadeInObjective.transform.position = new Vector3(moveAmt * 50, fadeInObjective.transform.position.y, fadeInObjective.transform.position.z);

            yield return 0;
        }
    }
}
