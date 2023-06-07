using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{

    [SerializeField] UnityEvent buttonPress;
    [SerializeField] UnityEvent buttonRelease;

    [SerializeField] Material pressedColor;
    [SerializeField] Material releasedColor;


    [SerializeField] Renderer buttonRenderer;

    int count;


    private void OnTriggerEnter(Collider other)
    {
        if(count == 0)
        {
            buttonRenderer.material = pressedColor;
            buttonPress?.Invoke();
        }
        count++;
    }

    private void OnTriggerExit(Collider other)
    {
        count--;
        if(count == 0)
        {
            buttonRenderer.material = releasedColor;
            buttonRelease?.Invoke();
        }


    }

}