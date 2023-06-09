using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountedActivationScript : MonoBehaviour
{


    [SerializeField] int desiredCount;

    [SerializeField] UnityEvent activationEvent;


    int count;
    bool activated;

    private void Start()
    {
        activated = false;
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





}
