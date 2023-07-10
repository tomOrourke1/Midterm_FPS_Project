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

           // Debug.LogError("box: " + tr.size + " scale: " + transform.localScale + " mult: " + VecMult(transform.localScale, tr.size));

            Vector3 bounds = (VecMult(transform.localScale, tr.size)) / 2f;


            Vector3 pos = tr.center + transform.position;


            Collider[] objs = Physics.OverlapBox(pos, bounds);

            count = objs.Length;

            Debug.LogError("Before removal: " +count);
            foreach (Collider obj in objs)
            {
                if (obj.GetComponent<IEntity>() == null && !obj.CompareTag("Player"))
                {
                    // Everything that isn't an entity or the player will be ignored
                    count--;
                }
            }

            Debug.LogError("after Removal: " +count);

            if (count == 0 && !activated)
            {
                Exit();
            }
        }
    }

    Vector3 VecMult(Vector3 v, Vector3 v2)
    {
        Vector3 value;
        value.x = v.x * v2.x;
        value.y = v.y * v2.y;
        value.z = v.z * v2.z;
        return value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }



        if (count == 0)
        {
            Debug.Log("Enter");
            buttonRenderer.material = pressedColor;
            buttonPress?.Invoke();
            activated = false;
            count++;
        }


    }

    void Exit()
    {

        if (count == 0)
        {
            Debug.Log("Exit");
            buttonRenderer.material = releasedColor;
            buttonRelease?.Invoke();
            activated = true;
        }
    }


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.isTrigger)
    //    {
    //        return;
    //    }

    //    //count--;

    //    if (count == 0)
    //    {
    //        Exit();
    //    }
    //}

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
