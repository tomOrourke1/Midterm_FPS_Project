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
        if(gameManager.instance.GetKeyCounter() > 0 && door.GetLockedStatus())
        {
            door.SetLockStatus(false);
            gameManager.instance.SetKeyCounter(gameManager.instance.GetKeyCounter() - 1);
        }

        if(count == 0)
        {
            door.Activate();
        }
        count++;
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
