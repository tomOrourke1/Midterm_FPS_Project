using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum  SpawnAbles
{
    basicEnemy,
    machineGunner,
    turret,
    scientist,
    key,
    box
}


public class RoomManager : MonoBehaviour
{


    // list of objects to spawn by tpype and position and rotation


    // reset room
    // spawn room
    // despawn room
    // player spawn point

    // doors locked status
    // read the room function



    [SerializeField] Transform boxTrans;
    [SerializeField] BoxCollider box;
    [SerializeField] LayerMask mask;




    void ReadTheRoom()
    {

        var objs = Physics.BoxCastAll(boxTrans.position, box.size / 2, Vector3.zero, boxTrans.rotation, 0, mask);


        foreach (var obj in objs)
        {

        }

        
    }
    




}
