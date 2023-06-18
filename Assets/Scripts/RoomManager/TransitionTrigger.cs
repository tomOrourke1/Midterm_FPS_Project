using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    [SerializeField] DoorScript DoorToLock;
    [SerializeField] RoomManager NextRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LockDoor();
            UnloadRoom();
            SetCurrentRoom();
            StartCurrentRoom();
            enabled = false;
        }
    }

    private void LockDoor()
    {
        if (DoorToLock != null)
        {
            //DoorToLock.GetComponent<DoorDetectPlayerInProximity>().enabled = false;

            DoorToLock.SetLockStatus(true);

            if (DoorToLock.GetOpenStatus())
            {
                DoorToLock.CloseLockedDoor();
            }
        }
    }

    private void UnloadRoom()
    {
        if (GameManager.instance.GetCurrentRoomManager() != null)
        {
            GameManager.instance.GetCurrentRoomManager().UnloadRoom();
        }
    }

    private void SetCurrentRoom()
    {
        GameManager.instance.SetCurrentRoomManager(NextRoom);
    }

    private void StartCurrentRoom()
    {
        GameManager.instance.GetCurrentRoomManager().StartRoom();
    }



}
