using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class TutorialSpawnables : MonoBehaviour
{



    [SerializeField] GameObject spawnable;
    [SerializeField] Transform partent;

    [SerializeField] bool useNavmesh;

    public void SpawmItem()
    {

        if (useNavmesh)
        {

            Vector3 point = Random.insideUnitSphere * 3 + transform.position;

            NavMeshHit hit;
            bool doesHit = NavMesh.SamplePosition(point, out hit, 10, 1);

            if (doesHit)
            {

                Instantiate(spawnable, hit.position, transform.rotation, partent);

            }
        }
        else
        {
            Instantiate(spawnable, transform.position, transform.rotation, partent);

        }




    }



}
