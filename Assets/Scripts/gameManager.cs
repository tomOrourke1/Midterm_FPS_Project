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

    // Get/Set area, for any private variable that needs to be retrieved and/set then do it through
    // a Get/Set function

    public int GetKeyCounter()
    {
        return KeyCounter;
    }
    public GameObject GetPlayerSpawnPOS()
    {
        return PlayerSpawnPOS;
    }

    public void SetKeyCounter(int counter)
    {
        KeyCounter = counter;
    }

    public void SetPlayerSpawnPos(GameObject _spawnPosGameObj)
    {
        PlayerSpawnPOS = _spawnPosGameObj;
    }

    public float GetOriginalTimeScale()
    {
        return timescaleOrig;
    }

    public GameObject GetPlayerObj()
    {
        return player;
    }

    public Player GetPlayerScript()
    {
        return playerscript;
    }

    public PlayerResources GetPlayerResources()
    {
        return playerResources;
    }
}
