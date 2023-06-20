using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawners : MonoBehaviour, IEntitySpawner
{
    [Header("----- Spawned Entity -----")]
    [SerializeField] GameObject Entity;

    private void Start()
    {
        // Maybe remove this later when Room Manager can Detect when the player enters the room
        GetObject();
    }

    public GameObject GetObject()
    {
        return Entity;
        //Instantiate(Entity, transform.position, transform.rotation);
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}
