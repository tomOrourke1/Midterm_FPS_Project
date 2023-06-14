using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("-----Player Stuff-----")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject PlayerSpawnPOS;
    [SerializeField] Player playerscript;
    [SerializeField] PlayerResources playerResources;

    [Header("Objective Items")]
    [SerializeField] int KeyCounter;

    private float timescaleOrig;

    void Awake()
    {
        instance = this;
        timescaleOrig = Time.timeScale;

        // Sets both the player and player script
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent<Player>();
        playerResources = player.GetComponent<PlayerResources>();
        PlayerSpawnPOS = GameObject.FindGameObjectWithTag("Player Spawn Pos");
    }

    private void Start()
    {
        timescaleOrig = Time.timeScale;
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
    /// Respawn the player, reset the current room. 
    /// </summary>
    public void RespawnCaller()
    {

    }
}
