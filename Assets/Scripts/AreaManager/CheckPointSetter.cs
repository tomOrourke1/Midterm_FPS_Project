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
            if (CheckPointPos != GameManager.instance.GetPlayerSpawnPOS() || GameManager.instance.GetPlayerSpawnPOS() == null)
            {
                Debug.Log("Point Set");
                SetCheckPoint();
                UIManager.instance.SaveIcon();
            }
        }
    }

    //Sets the players spawn point to the checkpoint they touch
    void SetCheckPoint() 
    {
        GameManager.instance.SetPlayerSpawnPos(CheckPointPos); 
    }
}
