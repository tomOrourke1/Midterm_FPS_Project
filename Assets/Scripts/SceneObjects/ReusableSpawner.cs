using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReusableSpawner : MonoBehaviour
{


    [Header("-- spawner --")]
    [SerializeField] GameObject spawnable;

    [SerializeField] Transform spawnTransform;

    [HideInInspector] 
    public GameObject currentObject;


    private void Start()
    {
        RespawnObject();
    }


    public void RespawnObject()
    {
        if(currentObject != null)
            Destroy(currentObject);
        currentObject = Instantiate(spawnable, spawnTransform.position, Quaternion.identity);
    }


}
