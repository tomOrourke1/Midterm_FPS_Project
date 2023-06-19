using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CustomPool : MonoBehaviour
{
    [Header("Object Pooling Components")]
    [SerializeField] List<GameObject> pool;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int poolCount;
    [SerializeField] int minAmount;
    [SerializeField] int maxAmount;


    /*     
           Object pool requirements
           
   Done    store objects in pool
           
   Done    Get object in pool
   Done    - and activate it
           
   Not     turn off object in pool
  Entirely - reset values on the object
   Done    
           
  Done-    max Amount of objects
  Done-    initial amount of objects
           
   Not     Generate more objects if need be
   Done    - to less <= the object cap 


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
