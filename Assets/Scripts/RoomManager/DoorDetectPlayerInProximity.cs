using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorDetectPlayerInProximity : MonoBehaviour, IEnvironment
{
    [SerializeField] IDoorActivator door;

    int count;

    private void Start()
    {
        door = transform.parent.GetComponent<IDoorActivator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (blackList(other))
        {
            return;
        }

        if (door.GetLockedStatus())
        {
            bool player = other.CompareTag("Player");
            if (player)
            {
                if (GameManager.instance.GetKeyChain().GetKeys() > 0)
                {
                    door.SetLockStatus(false);
                    GameManager.instance.GetKeyChain().removeKeys(1);
                }
            }
        }

        if (count == 0)
        {
            door.Activate();
        }

        count++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (blackList(other))
        {
            return;
        }

        count--;
        if (count == 0)
        {
            door.Activate();
        }
    }

    // This is a function tied to IEnvironment meant to be used to reset a room
    public void ResetObject()
    {
        count = 0;
    }

    public void StartObject()
    {
        // Nothing needs to happen here
    }

    public void StopObject()
    {
        // Nothing needs to happen here
    }

    bool blackList(Collider other)
    {
        return other.CompareTag("Enemy Bullet") || other.CompareTag("PlayerSpecial") || other.CompareTag("Enemy");
    }
}
