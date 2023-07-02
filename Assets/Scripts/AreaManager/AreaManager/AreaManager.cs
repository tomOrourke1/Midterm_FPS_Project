using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AreaManager : MonoBehaviour
{
    // list of objects to spawn by type, position and rotation

    // reset room
    // spawn room
    // despawn room
    // player spawn point

    // doors locked status
    // read the room function

    enum RoomType
    {
        Default = 0,
        Key,
        Encounter,
        Elite,
        Puzzle
    }

    [Header("Place the start and end doors to your area")]
    [SerializeField] DoorScript EntryDoor;
    [SerializeField] DoorScript ExitDoor;


    [Header("Select the type of area this is")]
    [SerializeField] RoomType roomType;


    [Header("Place objective to track below")]
    [SerializeField] GameObject TrackedObject;


    List<GameObject> Entities = new List<GameObject>();
    List<IEntity> entities;
    List<IEnvironment> environment;
    List<EntitySpawners> Spawners;

    int KillCounter = 0;
    bool EliteIsDead = false;
    bool PuzzleIsComplete = false;

    bool ObjectiveComplete = false;

    private void Start()
    {
        EntryDoor.GetComponentInChildren<DoorDetectPlayerInProximity>().EnterTransitionMode();

        KillEntities();
        StopEnvironments();

        SetObjective();
    }

    private void Update()
    {
        if (this == GameManager.instance.GetCurrentRoomManager())
        {
            if (ObjectiveComplete)
            {
                UnlockExit();
            }
            else
            {
                CheckObjective();
            }
        }

    }

    void UnlockExit()
    {
        ExitDoor.SetLockStatus(false);
    }

    void LockExit()
    {
        ExitDoor.CloseDoor();
        ExitDoor.SetLockStatus(true);
    }

    void CheckObjective()
    {
        switch (roomType)
        {
            case RoomType.Encounter:

                if (KillCounter == 0)
                {
                    ObjectiveComplete = true;
                }

                break;

            case RoomType.Elite:

                CheckObjectiveElite();

                if (EliteIsDead)
                {
                    ObjectiveComplete = true;
                }

                break;

            //case RoomType.Puzzle:

            //    CheckObjectivePuzzle();

            //    if (PuzzleIsComplete)
            //    {
            //        ObjectiveComplete = true;
            //    }

            //    break;
        }
    }

    void SetObjective()
    {
        KillCounter = 0;
        EliteIsDead = false;
        PuzzleIsComplete = false;

        ObjectiveComplete = false;


        switch (roomType)
        {
            case RoomType.Default:

                ObjectiveComplete = true;
                break;

            case RoomType.Key:
                // Keys need no tracking
                break;

            case RoomType.Encounter:
                SetObjectiveEncounter();
                break;

            case RoomType.Elite:
                SetObjectiveElite();
                break;

            case RoomType.Puzzle:
                SetObjectivePuzzle();
                break;

            default:

                ObjectiveComplete = true;
                break;
        }
    }

    void SetObjectiveEncounter()
    {
        // Set a kill counter
        ReadTheRoom();
        foreach (var entity in Entities)
        {
            if (entity.GetComponent<EnemyBase>() != null)
            {
                KillCounter++;
            }
        }
    }

    void SetObjectiveElite()
    {
        // Makes sure that the Tracked object isn't null
        TrackedObject.GetComponent<EntitySpawners>();
    }
    void CheckObjectiveElite()
    {
        ObjectiveComplete = TrackedObject.GetComponent<EntitySpawners>().IsMyEnemyDead();
    }

    void SetObjectivePuzzle()
    {
        // Set a puzzle tracker to watch

    }
    void CheckObjectivePuzzle()
    {
        // IPuzzle.IsComplete()
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

    public void DecrementCounter()
    {
        KillCounter--;
    }

    void SpawnEntities()
    {
        ReadTheRoom();

        for (int i = 0; i < Entities.Count; i++)
        {
            Spawners[i].ResetEnemyDeath();

            if (Entities[i].GetComponent<EnemyBase>() != null)
            {
                EnemyBase eBase;

                // Store enemy in spawner if entity is an enemy
                eBase = Instantiate(Entities[i], Spawners[i].GetTransform().position, Spawners[i].GetTransform().rotation, gameObject.transform).GetComponent<EnemyBase>();

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

        LockExit();
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
        SetObjective();

        StopEnvironments();

        KillEntities();
        KillEntities();

        StartEnvironments();
        SpawnEntities();
    }

    public void LeaveArea()
    {
        LockExit();
    }
}
