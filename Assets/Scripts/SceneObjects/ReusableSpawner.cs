using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReusableSpawner : MonoBehaviour, IEnvironment
{


    [Header("-- spawner --")]
    [SerializeField] GameObject spawnable;

    [SerializeField] Transform spawnTransform;

    [HideInInspector] 
    public GameObject currentObject;

    bool Activated;

    private void Awake()
    {
        Activated = false;
        //RespawnObject();
    }

    private void Update()
    {
        if (currentObject == null && Activated)
        {
            RespawnObject();

        }
    }

    public void RespawnObject()
    {
        if (currentObject != null)
            Destroy(currentObject);
        currentObject = Instantiate(spawnable, spawnTransform.position, Quaternion.identity);
    }

    public void StopObject()
    {
        Activated = false;
        Destroy(currentObject);
    }

    public void StartObject()
    {
        Activated = true;
        RespawnObject();
    }
}
