using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CustomPool : MonoBehaviour
{
    [Header("Object Pooling Components")]
    [SerializeField] List<GameObject> pool;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int poolCount;



    /*
    Object pool requirements
    
    store objects in pool
    
    Get object in pool
    - and activate it

    turn off object in pool
    - reset values on the object

    max Amount of objects
    initial amount of objects

    Generate more objects if need be
    - to less <= the object cap 




    */



    private void Start()
    {
        pool = new List<GameObject>();
        GameObject tempAdd;
        for (int i = 0; i < poolCount; i++)
        {
            tempAdd = Instantiate(objectToPool);
            tempAdd.SetActive(false);
            pool.Add(tempAdd);
        }
    }

    public GameObject GetPooledObject()
    {

        for (int i = 0; i < poolCount; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }

    public void ReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public int GetPoolCount()
    {
        return poolCount;
    }

    public GameObject ReturnObject(Transform loc)
    {
        
        return null;
    }

}
