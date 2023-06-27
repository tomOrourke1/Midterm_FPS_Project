using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AreaManager : MonoBehaviour
{
    // list of objects to spawn by tpype and position and rotation

    // reset room
    // spawn room
    // despawn room
    // player spawn point

    // doors locked status
    // read the room function

    [SerializeField] List<GameObject> Entities = new List<GameObject>();
    // Just the IEntity of each entity that is currently alive
    List<IEntity> entities;
    List<IEnvironment> environment;
    List<EntitySpawners> Spawners;

    private void Start()
    {
        KillEntities();
        StopEnvironments();
    }

    public List<EntitySpawners> GetEntitySpawners()
    {
        return Spawners;
    }

    void ReadTheRoom()
    {
        // Stores the spawns
        if (Spawners != null)
        {
            Spawners.Clear();
            Entities.Clear();
        }
        Spawners = new List<EntitySpawners>(gameObject.GetComponentsInChildren<EntitySpawners>());

        foreach (var obj in Spawners)
        {
            // Stores the entity that is spawned at each of the spawners.
            Entities.Add(obj.GetObject());
        }
    }

    void SpawnEntities()
    {
        ReadTheRoom();

        for (int i = 0; i < Entities.Count; i++)
        {
            Spawners[i].ResetEnemyDeath();

            if (Entities[i].GetComponent<EnemyBase>() != null)
            {
                // Store enemy in spawner if entity is an enemy
                var eBase = Instantiate(Entities[i], Spawners[i].GetTransform().position, Spawners[i].GetTransform().rotation, gameObject.transform).GetComponent<EnemyBase>();

                // this needs to be able to assign it's death to do something.
                // but I don't like how it is currently connected.

                eBase.HealthPool.OnResourceDepleted += Spawners[i].MyEnemyDied;
            }
            else
            {
                Instantiate(Entities[i], Spawners[i].GetTransform().position, Spawners[i].GetTransform().rotation, gameObject.transform);
            }
        }
    }

    void KillEntities()
    {
        entities = new List<IEntity>(gameObject.GetComponentsInChildren<IEntity>());
        
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Respawn();
        }
    }

    void StopEnvironments()
    {
        environment = new List<IEnvironment>(gameObject.GetComponentsInChildren<IEnvironment>(true));
        
        for (int i = 0; i < environment.Count; i++)
        {
            environment[i].StopObject();
        }
    }

    void StartEnvironments()
    {
        environment = new List<IEnvironment>(gameObject.GetComponentsInChildren<IEnvironment>(true));

        for (int i = 0; i < environment.Count; i++)
        {
            environment[i].StartObject();
        }
    }

    public void UnloadRoom()
    {
        StopEnvironments();
        KillEntities();
    }

    public void StartRoom()
    {
        StartEnvironments();
        SpawnEntities();
    }

    public void Respawn()
    {
        StopEnvironments();

        KillEntities();
        KillEntities();

        StartEnvironments();
        SpawnEntities();
    }
}
