using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountedActivationScript : MonoBehaviour, IEnvironment
{
    [SerializeField] int desiredCount;

    [SerializeField] UnityEvent activationEvent;
    [SerializeField] UnityEvent deactivationEvent;

    [SerializeField] bool Reverseable;


    int count;
    bool activated;

    private void Start()
    {
        activated = false;
        count = 0;
    }

    public void Increment()
    {
        count++;

        if (count >= desiredCount && !activated)
        {
            activationEvent?.Invoke();
            activated = true;
        }
    }

    public void Decrement()
    {
        count--;

        if (count < desiredCount)
        {
            if (Reverseable)
            {
                deactivationEvent?.Invoke();
                activated = false;
            }
        }
    }

    public void StopObject()
    {

    }

    public void StartObject()
    {
        count = 0;
        activated = false;
    }
}
