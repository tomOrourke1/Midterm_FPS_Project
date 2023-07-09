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
        [Tooltip("Does nothing to the room. Keeps room in state it was in during editor.")]
        Default = 0,
        [Tooltip("Locks the door, requires a 'KEY TERMINAL' for the player to unlock the door.")]
        Key,
        [Tooltip("Locks the door, keeps track of all 'ENEMY SPAWNERS'. Requires all enemies to be defeated for Exit Door to be unlocked.")]
        Encounter,
        [Tooltip("Locks the door, Keeps track of the 'ELITE SPAWNER' entity and unlocks the door when they entity is killed.")]
        Elite,
        [Tooltip("Locks the door, requires a 'PUZZLE METHOD' to unlock the door.")]
        Puzzle
    }

    [Header("Place the start and end doors to your area")]
    [Tooltip("The entrance to the room.")]
    [SerializeField] DoorScript EntryDoor;
    [Tooltip("The exit to the room.")]
    [SerializeField] DoorScript ExitDoor;


    [Header("Select the type of area this is")]
    [Tooltip("This determines how the area manager handles the room.")]
    [SerializeField] RoomType roomType;


    [Header("Place objective to track below")]
    [Tooltip("If you are using an elite room, place the spawner of the elite enemy here.")]
    [SerializeField] GameObject EliteSpawner;


    List<GameObject> Entities = new List<GameObject>();
    List<IEntity> entities;
    List<IEnvironment> environment;
    List<EntitySpawners> Spawners;

    int KillCounter = 0;
    bool EliteIsDead = false;
    //bool PuzzleIsComplete = false;

    bool ObjectiveComplete = false;

    private void Awake()
    {
        EntryDoor.GetComponentInChildren<DoorDetectPlayerInProximity>().EnterTransitionMode(this);
        
    }

    private void Start()
    {

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
        //PuzzleIsComplete = false;

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
        EliteSpawner.GetComponent<EntitySpawners>();
    }
    void CheckObjectiveElite()
    {
        ObjectiveComplete = EliteSpawner.GetComponent<EntitySpawners>().IsMyEnemyDead();
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

    public void ReadTheRoom()
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
                GameObject spawned = Instantiate(Entities[i], Spawners[i].GetTransform().position, Spawners[i].GetTransform().rotation, gameObject.transform);

                if (spawned.GetComponent<KeyScript>() != null)
                {
                    spawned.GetComponent<KeyScript>().SetSpawner(i);
                }
            }
        }
    }

    public void CallDeath(int i)
    {
        Spawners[i].MyEnemyDied();
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
