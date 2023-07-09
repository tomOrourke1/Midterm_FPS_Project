using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class WireManager : MonoBehaviour, IEnvironment
{
    [SerializeField] UnityEvent Activate;
    [SerializeField] UnityEvent Deactivate;

    [SerializeField] Material Activated;
    [SerializeField] Material Inate;

    [SerializeField] float Delay;

    bool powered = false;
    bool waiting = false;
    bool activated = false;

    bool canRunCO;

    int i;

    WireSegment[] segments;

    private void Awake()
    {
        waiting = false;
        segments = GetComponentsInChildren<WireSegment>();
    }

    private void Start()
    {
        i = 0;
        foreach (WireSegment segment in segments)
        {
            segment.GetComponent<MeshRenderer>().material = Inate;
        }
    }

    private void Update()
    {
        if (segments[segments.Length - 1].IsActivated() && !activated)
        {
            Activate.Invoke();
            activated = true;
        }

        if (!segments[segments.Length - 1].IsActivated() && activated)
        {
            Deactivate.Invoke();
            activated = false;
        }

        HandlePower();
    }



    void HandlePower()
    {
        if (powered)
        {
            if (!canRunCO)
            {
                canRunCO = true;
                StartCoroutine(wait());
            }
        }
        else
        {
            foreach (WireSegment segment in segments)
            {
                segment.TurnOff(Inate);
            }
            i = 0;
        }
    }

    IEnumerator wait()
    {
        while (i < segments.Length)
        {
            segments[i].TurnOn(Activated);
            waiting = true;
            i++;
            
            yield return new WaitForSeconds(Delay);
        }
        waiting = false;
        canRunCO = false;
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
        PowerOff();
        waiting = false;
    }

    public void StartObject()
    {
        i = 0;
        foreach (WireSegment segment in segments)
        {
            segment.GetComponent<MeshRenderer>().material = Inate;
        }
    }
}
