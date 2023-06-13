using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawners : MonoBehaviour, IEntitySpawner
{
    [Header("----- Spawned Entity -----")]
    [SerializeField] GameObject Entity;

    private void Start()
    {
        // Maybe remove this later when Room Manager can Detect when the player enters the room
        Spawn();
    }

    public void Spawn()
    {
        Instantiate(Entity, transform.position, transform.rotation);
    }
}
