using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WireSegment : MonoBehaviour
{
    [SerializeField] Transform BaseWire;
    [SerializeField] Transform PoweredWire;

    bool activated = false;

    public bool Activate(float growRate)
    {
        float newSize = PoweredWire.transform.localScale.y + growRate * Time.deltaTime;

        newSize = Mathf.Clamp(newSize, 0.0f, BaseWire.transform.localScale.y);

        PoweredWire.transform.localScale = new Vector3(PoweredWire.transform.localScale.x, newSize, PoweredWire.transform.localScale.z);

        if (PoweredWire.transform.localScale.y >= BaseWire.transform.localScale.y)
        {
            activated = true;
        }

        return activated;
    }

    public void Deactivate()
    {
        PoweredWire.transform.localScale = new Vector3(PoweredWire.transform.localScale.x, 0, PoweredWire.transform.localScale.z);

        activated = false;
    }

    public bool IsActivated() { return activated; }
}
