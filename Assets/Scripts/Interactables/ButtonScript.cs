using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour, IEnvironment
{

    [SerializeField] UnityEvent buttonPress;
    [SerializeField] UnityEvent buttonRelease;

    [SerializeField] Material pressedColor;
    [SerializeField] Material releasedColor;


    [SerializeField] Renderer buttonRenderer;

    int count;


    private void OnTriggerEnter(Collider other)
    {
        if(count == 0 && !other.isTrigger)
        {
            buttonRenderer.material = pressedColor;
            buttonPress?.Invoke();
        }
        count++;
    }

    private void OnTriggerExit(Collider other)
    {
        count--;
        if(count == 0 && !other.isTrigger)
        {
            buttonRenderer.material = releasedColor;
            buttonRelease?.Invoke();
        }


    }

    public void StartObject()
    {
        count = 0;
        buttonRenderer.material = releasedColor;
    }

    public void StopObject()
    {
        count = 0;
    }
}
