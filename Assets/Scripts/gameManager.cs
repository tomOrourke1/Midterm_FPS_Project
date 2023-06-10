using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public PlayerResources playerResources;
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
    [SerializeField] int KeyCounter;

    [Header("Scene Transitioning")]
    [SerializeField] Image sceneFader;

    private RadialMenu radialMenuScriptRef;
    private float timescaleOrig;
    private int enemiesRemaining;

    public MenuState menuState;

    void Awake()
    {
        instance = this;
        timescaleOrig = Time.timeScale;

        // Sets both the player and player script
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent<Player>();
        playerResources = player.GetComponent<PlayerResources>();

        PlayerSpawnPOS = GameObject.FindGameObjectWithTag("Player Spawn Pos");

        radialMenuScriptRef = GetComponent<RadialMenu>();
        pStatsUI = GetComponent<PlayerStats_UI>();



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
    }

    // Sets game to paused state
    public void Paused()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        menuState = MenuState.none;
    }

    // resumes game while paused
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

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(3);
        activeMenu = winMenu;
        activeMenu.SetActive(true);
        Paused();
        UI_Manager.instance.EnableBoolAnimator(UI_Manager.instance.WinPanel);
    }

    public void LoseGame()
    {
        Paused();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        UI_Manager.instance.EnableBoolAnimator(UI_Manager.instance.LossPanel);
    }


    private void DisableMenus()
    {
        pausemenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
    }






    // Get/Set area, for any private variable that needs to be retrieved and/set then do it through
    // a Get/Set function

    public int GetKeyCounter()
    {
        return KeyCounter;

    }
    public Image GetFlashImage()
    {
        return flashDamage.GetComponent<Image>();
    }

    public GameObject GetPlayerSpawnPOS()
    {
        return PlayerSpawnPOS;
    }

    public Image GetSceneFader()
    {
        return sceneFader;
    }

    public void SetKeyCounter(int counter)
    {
        KeyCounter = counter;
    }

    public void SetPlayerSpawnPos(GameObject _spawnPosGameObj)
    {
        PlayerSpawnPOS = _spawnPosGameObj;
    }
}
