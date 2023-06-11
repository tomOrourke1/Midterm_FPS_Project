using System.Collections.Generic;
using UnityEngine;

public class CustomPool : MonoBehaviour
{
    [Header("Object Pooling Components")]
    [SerializeField] List<GameObject> pool;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int poolCount;

    private void Start()
    {
        ResetObj = ReturnObject;
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
        Transform temp = null;
        ResetObj(temp);
        return poolCount;
    }

    public GameObject ReturnObject(Transform loc)
    {
        
        return null;
    }

    public delegate GameObject ResetObject(Transform loc);
    public ResetObject ResetObj;
}
