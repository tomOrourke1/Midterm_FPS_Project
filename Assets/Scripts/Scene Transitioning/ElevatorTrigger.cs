using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] DoorScript doorToLock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger || !other.CompareTag("Player"))
            return;

        doorToLock.SetLockStatus(true);
        doorToLock.CloseLockedDoor();
    }
}
