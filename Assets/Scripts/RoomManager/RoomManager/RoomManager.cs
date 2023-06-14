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
    List<IEntitySpawner> Spawners;

    private void Start()
    {
        ReadTheRoom();
        SpawnEntities();
    }

    void ReadTheRoom()
    {
        // Stores the spawns
        //var objs = Physics.BoxCastAll(boxTrans.position, box.size / 2, Vector3.zero, boxTrans.rotation, 0, mask);

        Spawners = new List<IEntitySpawner>(gameObject.GetComponentsInChildren<IEntitySpawner>());

        foreach (var obj in Spawners)
        {
            // Stores the entity that is spawned at each of the spawners.
            Entities.Add(obj.GetObject());

            // Stores the Locations of each of the spawners
            //Spawners.Add(obj.collider.transform);
        }

        //Debug.Log(objs.Count());
    }

    void SpawnEntities()
    {
        for (int i = 0; i < Entities.Count; i++)
        {
            Instantiate(Entities[i], Spawners[i].GetTransform().position, Spawners[i].GetTransform().rotation);
        }
    }




}
