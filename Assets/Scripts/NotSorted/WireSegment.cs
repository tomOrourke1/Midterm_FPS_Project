using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WireSegment : MonoBehaviour
{
    bool activated = false;

    public void TurnOn(Material Activated)
    {
        gameObject.GetComponent<MeshRenderer>().material = Activated;
        activated = true;
    }

    public void TurnOff(Material Inate) 
    {
        gameObject.GetComponent<MeshRenderer>().material = Inate;
        activated = false;
    }

    public bool IsActivated() { return activated; }
}
