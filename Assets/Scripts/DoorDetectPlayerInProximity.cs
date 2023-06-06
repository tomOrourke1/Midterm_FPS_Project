using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetectPlayerInProximity : MonoBehaviour
{
    [SerializeField] IDoorActivator door;


    private void Start()
    {
        door = transform.parent.GetComponent<IDoorActivator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        door.Activate();
    }

    private void OnTriggerExit(Collider other)
    {
        door.Activate();
    }
}
