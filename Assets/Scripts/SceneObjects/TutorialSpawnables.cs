using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawnables : MonoBehaviour
{



    [SerializeField] GameObject spawnable;
    [SerializeField] Transform partent;


    public void SpawmItem()
    {
        Instantiate(spawnable, transform.position, transform.rotation, partent);


    }



}
