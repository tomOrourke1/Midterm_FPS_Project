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
    bool activated = false;

    private void Update()
    {
        if (count != 0)
        {
            var tr = GetComponent<BoxCollider>();

            Vector3 bounds = tr.size;
            Vector3 pos = tr.center + transform.position;


            Collider[] objs = Physics.OverlapBox(pos, bounds / 2);

            

            count = objs.Length;
            if (count == 0 && !activated)
            {
                Exit();
                activated = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        activated = false;
        if (count == 0)
        {
            buttonRenderer.material = pressedColor;
            buttonPress?.Invoke();
        }


        count++;
    }

    void Exit()
    {

        if (count == 0)
        {
            buttonRenderer.material = releasedColor;
            buttonRelease?.Invoke();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        //count--;

        if (count == 0)
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
