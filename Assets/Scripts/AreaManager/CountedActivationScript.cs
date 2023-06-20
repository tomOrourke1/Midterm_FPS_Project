using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountedActivationScript : MonoBehaviour, IEnvironment 
{


    [SerializeField] int desiredCount;

    [SerializeField] UnityEvent activationEvent;


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

        if(count >= desiredCount && !activated)
        {
            activationEvent?.Invoke();
            activated = true;
        }
    }

    public void Decrement() 
    { 
        count--; 

        if(count < desiredCount)
        {
            activated = false;
        }
    }

    public void ResetObject()
    {
        count = 0;
        activated = false;
    }

    public void StopObject()
    {

    }

    public void StartObject()
    {

    }
}
