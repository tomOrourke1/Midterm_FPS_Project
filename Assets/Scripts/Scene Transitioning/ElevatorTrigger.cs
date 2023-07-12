using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] DoorScript doorToLock;

    private void OnTriggerEnter(Collider other)
    {
        doorToLock.SetLockStatus(true);
        doorToLock.CloseLockedDoor();
    }
}
