using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountedActivationScript : MonoBehaviour, IEnvironment
{
    [SerializeField] int desiredCount;

    [SerializeField] UnityEvent activationEvent;
    [SerializeField] UnityEvent deactivationEvent;
    [SerializeField] MeshRenderer lightActivated;
    [SerializeField] Material activeMat;
    [SerializeField] Material deactiveMat;

    [SerializeField] bool Reverseable;


    int count;
    bool activated;

    private void Start()
    {
        activated = false;
        count = 0;
        SetLight();
    }

    public void Increment()
    {
        count++;

        if (count >= desiredCount && !activated)
        {
            activationEvent?.Invoke();
            activated = true;
        }
        SetLight();
    }

    public void Decrement()
    {
        Debug.LogWarning("Decrement");
        count--;

        if (count < desiredCount)
        {
            if (Reverseable)
            {
                deactivationEvent?.Invoke();
                activated = false;
            }
        }
        SetLight();
    }

    public void StopObject()
    {
        Debug.Log("Stop: " + count);
        count = 0;
        activated = false;
        SetLight();
    }

    public void StartObject()
    {
        Debug.Log("Start: " + count);
        count = 0;
        activated = false;
        SetLight();
    }

    private void SetLight()
    {
        if (activated)
        {
            lightActivated.material = activeMat;
        }
        else if (!activated)
        {
            lightActivated.material = deactiveMat;
        }
    }
}
