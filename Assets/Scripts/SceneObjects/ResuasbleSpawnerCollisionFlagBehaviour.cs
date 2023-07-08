using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResuasbleSpawnerCollisionFlagBehaviour : MonoBehaviour
{

    ReusableSpawner spawner;

    private void Start()
    {
        // has to go two levels up for it to work
        spawner = gameObject.transform.parent.parent.GetComponent<ReusableSpawner>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(spawner.currentObject))
        {
            spawner.RespawnObject();
        }
    }





}
