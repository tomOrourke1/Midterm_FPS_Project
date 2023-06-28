using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Settings Manager")]
    [SerializeField] SettingsManager settingsManager;
    [SerializeField] KinesisEnabler isEnabledScript;

    [Header("-----Player Stuff-----")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject PlayerSpawnPOS;
    [SerializeField] Player playerscript;
    [SerializeField] PlayerResources playerResources;
    [SerializeField] KeyChain keyChain;

    [Header("Objective Items")]
    [SerializeField] int KeyCounter;

    private float timescaleOrig;
    private AreaManager currentRoomManager;

    void Awake()
    {
        instance = this;
        timescaleOrig = Time.timeScale;

        // Sets both the player and player script
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent<Player>();
        playerResources = player.GetComponent<PlayerResources>();
        PlayerSpawnPOS = GameObject.FindGameObjectWithTag("Player Spawn Pos");
        keyChain = player.GetComponent<KeyChain>();
        settingsManager.UpdateObjectsToValues();
    }

    private void Start()
    {
        timescaleOrig = Time.timeScale;

        WakeUpKinesis();
    }

    /// <summary>
    /// When the game starts, kinesis are set to the enabled list of which types are on and off.
    /// When a kinesis gets obtained they are updated to the settings manager.
    /// </summary>
    private void WakeUpKinesis()
    {
        isEnabledScript.AeroSetActive(isEnabledScript.AeroEnabled());
        isEnabledScript.ElectroSetActive(isEnabledScript.ElectroEnabled());
        isEnabledScript.CryoSetActive(isEnabledScript.CryoEnabled());
        isEnabledScript.TeleSetActive(isEnabledScript.TeleEnabled());
        isEnabledScript.PyroSetActive(isEnabledScript.PyroEnabled());
    }

    /// <summary>
    /// Pauses Time.
    /// </summary>
    public void TimePause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Unpauses time to the original time scale.
    /// </summary>
    public void TimeUnpause()
    {
        Time.timeScale = timescaleOrig;
    }

    /// <summary>
    /// Runs the Time Pause and Mouse Unlock Functions.
    /// </summary>
    public void PauseMenuState()
    {
        TimePause();
        MouseUnlockShow();
    }

    /// <summary>
    /// Runs the Time Unpause and Mouse Lock and Hide Functions.
    /// </summary>
    public void PlayMenuState()
    {
        TimeUnpause();
        MouseLockHide();
    }

    /// <summary>
    /// Sets the mouse to visible and sets the cursors lock state to confined.
    /// </summary>
    public void MouseUnlockShow()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    /// <summary>
    /// Sets the mouse to hide and then locks it from moving.
    /// </summary>
    public void MouseLockHide()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Gets the key counter for the room manager.
    /// </summary>
    /// <returns></returns>
    public int GetKeyCounter()
    {
        return KeyCounter;
    }

    /// <summary>
    /// Returns the player spawn position as a game object. Used in the player script.
    /// </summary>
    /// <returns></returns>
    public GameObject GetPlayerSpawnPOS()
    {
        return PlayerSpawnPOS;
    }

    /// <summary>
    /// Sets the key amount to a certain value.
    /// </summary>
    /// <param name="counter">The value to change to.</param>
    public void SetKeyCounter(int counter)
    {
        KeyCounter = counter;
    }

    /// <summary>
    /// Moves the player spawn position to where the passed in object is.
    /// </summary>
    /// <param name="_spawnPosGameObj">Game Object to change the position to.</param>
    public void SetPlayerSpawnPos(GameObject _spawnPosGameObj)
    {
        PlayerSpawnPOS = _spawnPosGameObj;
    }

    /// <summary>
    /// Returns the original time scale that the game was in.
    /// </summary>
    /// <returns></returns>
    public float GetOriginalTimeScale()
    {
        return timescaleOrig;
    }

    /// <summary>
    /// Returns the player object. Used in many enemy based scripts.
    /// </summary>
    /// <returns></returns>
    public GameObject GetPlayerObj()
    {
        return player;
    }

    /// <summary>
    /// Returns the player script. Should be used in respawning.
    /// </summary>
    /// <returns></returns>
    public Player GetPlayerScript()
    {
        return playerscript;
    }

    /// <summary>
    /// Returns the keychain. Should be used for adding or removing keys.
    /// </summary>
    /// <returns></returns>
    public KeyChain GetKeyChain()
    {
        return keyChain;
    }

    /// <summary>
    /// Returns the players current position.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPlayerPOS()
    {
        return player.transform.position;
    }

    /// <summary>
    /// Returns the player resources script. 
    /// </summary>
    /// <returns></returns>
    public PlayerResources GetPlayerResources()
    {
        return playerResources;
    }

    /// <summary>
    /// Gets the key script.
    /// </summary>
    /// <returns></returns>
    public KeyChain ReturnKeyScript()
    {
        return player.GetComponent<KeyChain>();
    }

    /// <summary>
    /// Returns the settings manager script.
    /// </summary>
    /// <returns></returns>
    public SettingsManager GetSettingsManager()
    {
        return settingsManager;
    }
    
    /// <summary>
    /// Returns the Kinesis Enabler script.
    /// </summary>
    /// <returns></returns>
    public KinesisEnabler GetEnabledList()
    {
        return isEnabledScript;
    }

    /// <summary>
    /// Respawn the player, reset the current room. 
    /// </summary>
    public void RespawnCaller()
    {
        playerscript.RespawnPlayer();
        player.GetComponentInChildren<CameraController>().ResetCamera();
        currentRoomManager?.Respawn();
    }

    /// <summary>
    /// Sets the current room manager to the one that is passed in. 
    /// </summary>
    public void SetCurrentRoomManager(AreaManager roomManager)
    {
        currentRoomManager = roomManager;
    }

    /// <summary>
    /// Gets the current room manager. 
    /// </summary>
    public AreaManager GetCurrentRoomManager()
    {
        return currentRoomManager;
    }

    /// <summary>
    /// Nobody should touch this. Please don't.
    /// </summary>
    /// <returns></returns>
    public bool AllKinesisDisabled()
    {
        if (!isEnabledScript.CryoEnabled() && !isEnabledScript.AeroEnabled() && !isEnabledScript.TeleEnabled() && !isEnabledScript.PyroEnabled() && !isEnabledScript.ElectroEnabled())
            return true;
        else return false;
    }
}
