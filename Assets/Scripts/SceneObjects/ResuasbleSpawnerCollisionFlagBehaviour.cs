using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResuasbleSpawnerCollisionFlagBehaviour : MonoBehaviour
{

    ReusableSpawner spawner;

    private void Awake()
    {
        // has to go two levels up for it to work
        spawner = gameObject.transform.parent.parent.GetComponent<ReusableSpawner>();
    }


    private void OnTriggerEnter(Collider other)
    {
        MoveableObject Mo = other.gameObject.GetComponentInChildren<MoveableObject>();


        if (Mo != null && Mo.Equals(spawner.currentObject.GetComponentInChildren<MoveableObject>()))
        {
            spawner.RespawnObject();
        }
    }





}
