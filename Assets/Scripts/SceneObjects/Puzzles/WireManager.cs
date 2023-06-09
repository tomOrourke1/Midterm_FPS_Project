using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using static UnityEngine.ParticleSystem;

public class WireManager : MonoBehaviour, IEnvironment
{
    [SerializeField] UnityEvent Activate;
    [SerializeField] UnityEvent Deactivate;

    [SerializeField] float Delay;
    [SerializeField] float IncrementSize;

    bool powered;
    bool activated = false;

    bool canRunCO;

    int i;

    WireSegment[] segments;

    private void Awake()
    {
        powered = false;
        segments = GetComponentsInChildren<WireSegment>();
    }

    private void Start()
    {
        i = 0;
        foreach (WireSegment segment in segments)
        {
            segment.Deactivate();
        }
    }

    private void Update()
    {
        if (powered)
        {
            if (canRunCO)
            {
                StartCoroutine(ActivateAllWires());
            }
        }
        else
        {
            i = 0;
            canRunCO = true;

            foreach (WireSegment segment in segments)
            {
                segment.Deactivate();
            }

            StopAllCoroutines();
        }

        if (segments[segments.Length - 1].IsActivated() && !activated)
        {
            Activate.Invoke();
            activated = true;
        }
        else if (!segments[segments.Length - 1].IsActivated() && activated)
        {
            Deactivate.Invoke();
            activated = false;
        }
    }

    IEnumerator ActivateAllWires()
    {
        canRunCO = false;

        while (i < segments.Length)
        {
            if (segments[i].Activate(IncrementSize))
            {
                i++;
            }

            yield return new WaitForSeconds(Delay);
        }

        canRunCO = true;
    }

    public void PowerOn()
    {
        powered = true;
    }

    public void PowerOff()
    {
        powered = false;
    }

    public void StopObject()
    {
        StopAllCoroutines();
        PowerOff();
        powered = false;
        canRunCO = true;
    }

    public void StartObject()
    {
        i = 0;

        foreach (WireSegment segment in segments)
        {
            segment.Deactivate();
        }
    }
}
