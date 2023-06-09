using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSetter : MonoBehaviour
{
    private GameObject CheckPoint;
    [SerializeField] GameObject CheckPointPos;

    int KeysHeld = 0;

    private void OnTriggerEnter(Collider other)
    {
        //checks if player enters the collider
        if (other.CompareTag("Player"))
        {
            if (CheckPointPos != GameManager.instance.GetPlayerSpawnPOS() || GameManager.instance.GetPlayerSpawnPOS() == null)
            {
                SetCheckPoint();
                RemoveEnemySpawns();
                SaveKeys();
                UIManager.instance.SaveIcon();
            }
        }
    }

    //Sets the players spawn point to the checkpoint they touch
    void SetCheckPoint() 
    {
        GameManager.instance.SetPlayerSpawnPos(CheckPointPos); 
    }

    private void RemoveEnemySpawns()
    {
        List<EntitySpawners> spawners = GameManager.instance.GetCurrentRoomManager().GetEntitySpawners();

        for (int i = 0; i < spawners.Count; i++)
        {
            if (spawners[i].IsMyEnemyDead())
            {
                spawners[i].DisableSpawner();
            }
        }
    }

    private void SaveKeys()
    {
        //Debug.LogError("Save Keys");
        KeysHeld = GameManager.instance.GetKeyChain().GetKeys();
    }

    public int GetKeysHeld() { return KeysHeld; }
}
