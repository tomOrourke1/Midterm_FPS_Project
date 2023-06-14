using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RoomManager : MonoBehaviour
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
    List<IEntitySpawner> Spawners;

    private void Start()
    {
        ReadTheRoom();
        SpawnEntities();
        //StartCoroutine(kill());
    }

    //IEnumerator kill()
    //{
    //    yield return new WaitForSeconds(3);
    //    ClearRoom();
    //}

    void ReadTheRoom()
    {
        // Stores the spawns
        //var objs = Physics.BoxCastAll(boxTrans.position, box.size / 2, Vector3.zero, boxTrans.rotation, 0, mask);

        Spawners = new List<IEntitySpawner>(gameObject.GetComponentsInChildren<IEntitySpawner>());

        foreach (var obj in Spawners)
        {
            // Stores the entity that is spawned at each of the spawners.
            Entities.Add(obj.GetObject());
        }
    }

    void SpawnEntities()
    {
        for (int i = 0; i < Entities.Count; i++)
        {
            Instantiate(Entities[i], Spawners[i].GetTransform().position, Spawners[i].GetTransform().rotation, gameObject.transform);
        }
    }

    void ClearRoom()
    {
        entities = new List<IEntity>(gameObject.GetComponentsInChildren<IEntity>());

        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Respawn();
        }
    }


}
