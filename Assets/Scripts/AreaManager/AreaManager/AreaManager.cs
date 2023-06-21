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
    List<IEntitySpawner> Spawners;

    private void Start()
    {
        KillEntities();
        StopEnvironments();
    }

    IEnumerator kill()
    {
        yield return new WaitForSeconds(3);
        KillEntities();
    }

    void ReadTheRoom()
    {
        // Stores the spawns
        if (Spawners != null)
        {
            Spawners.Clear();
            Entities.Clear();
        }
        Spawners = new List<IEntitySpawner>(gameObject.GetComponentsInChildren<IEntitySpawner>());

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
            Instantiate(Entities[i], Spawners[i].GetTransform().position, Spawners[i].GetTransform().rotation, gameObject.transform);
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

    void ResetEnvironments()
    {
        environment = new List<IEnvironment>(gameObject.GetComponentsInChildren<IEnvironment>(true));
        for (int i = 0; i < environment.Count; i++)
        {
            environment[i].ResetObject();
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

        ResetEnvironments();
        SpawnEntities();
    }
}
