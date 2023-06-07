using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSetter : MonoBehaviour
{
    private GameObject CheckPoint;
    [SerializeField] GameObject CheckPointPos;

 
   
  
    private void OnTriggerEnter(Collider other)
    {
        //checks if player enters the collider
        if (other.CompareTag("Player"))
        {
            SetCheckPoint();
        }
    }

    //Sets the players spawn point to the checkpoint they touch
    void SetCheckPoint() 
    {
        gameManager.instance.PlayerSpawnPOS = CheckPointPos; 
    }
}
