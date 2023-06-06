using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSetter : MonoBehaviour
{
    private GameObject CheckPoint;
    Transform CheckPointPos;

 
   
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPoint.SetActive(true);
        }
    }
    void SetSpawnPoint() 
    {

    }
}
