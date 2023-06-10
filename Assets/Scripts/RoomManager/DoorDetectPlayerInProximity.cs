using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetectPlayerInProximity : MonoBehaviour
{
    [SerializeField] IDoorActivator door;

    int count;

    private void Start()
    {
        door = transform.parent.GetComponent<IDoorActivator>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if(door.GetLockedStatus())
        {
            bool player = other.CompareTag("Player");
            if(player)
            {
                if (GameManager.instance.GetKeyCounter() > 0)
                {
                    door.SetLockStatus(false);
                    GameManager.instance.SetKeyCounter(GameManager.instance.GetKeyCounter() - 1);
                }
            }
        }

        if(count == 0)
        {
            door.Activate();
        }

        if (!other.CompareTag("Enemy Bullet"))
        {
            count++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        count--;
        if(count == 0)
        {
            door.Activate();
        }
    }
}
