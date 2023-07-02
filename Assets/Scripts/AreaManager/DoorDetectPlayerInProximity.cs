using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorDetectPlayerInProximity : MonoBehaviour, IEnvironment
{
    [SerializeField] DoorScript door;

    int count;
    bool transitionMode = false;

    private void Start()
    {
        door = transform.parent.GetComponent<DoorScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transitionMode)
        {
            GameManager.instance.SetCurrentRoomManager(GetComponentInParent<AreaManager>());

            transitionMode = false;
        }

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
            door.OpenDoor();
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
            door.CloseDoor();
        }
    }

    public void EnterTransitionMode()
    {
        transitionMode = true;
    }

    public void StartObject()
    {
        count = 0;
    }

    public void StopObject()
    {

    }

    bool blackList(Collider other)
    {
        return other.CompareTag("Enemy Bullet") || other.CompareTag("PlayerSpecial") || other.CompareTag("Enemy");
    }
}
