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


    [SerializeField] BoxCollider boxCol;


    int count;
    bool activated = false;


    int initialCount;

    int lastCount;


    private void Start()
    {
        initialCount = GetOverlap().Length;
    }
    private void Update()
    {
        if (!activated)
            return;

        var h = GetOverlap();
        count = h.Length;


        if(count > lastCount && lastCount == initialCount)
        {
            buttonPress?.Invoke();
            buttonRenderer.material = pressedColor;
        }
        if(count == initialCount && lastCount > count)
        {
            // exit
            buttonRelease?.Invoke();
            buttonRenderer.material = releasedColor;
        }


        lastCount = count;
        count = initialCount;
    }


    Collider[] GetOverlap()
    {

        var mult = VecMult(transform.localScale, boxCol.center);
        var pos = transform.position + (transform.forward * mult.z + transform.right * mult.x + transform.up * mult.y);

        var size = VecMult(transform.localScale, boxCol.size);
        return Physics.OverlapBox(pos, size / 2, transform.localRotation);
    }



    Vector3 VecMult(Vector3 v, Vector3 v2)
    {
        Vector3 value;
        value.x = v.x * v2.x;
        value.y = v.y * v2.y;
        value.z = v.z * v2.z;
        return value;
    }



    //private void OnDrawGizmos()
    //{
    //    //var pos = transform.position + (transform.forward * (boxCol.center.z * transform.localScale.z) + transform.right * (boxCol.center.x * transform.localScale.x) + transform.up * (boxCol.center.y * transform.localScale.y));
    //    //var size = VecMult(transform.localScale, boxCol.size);


    //    var mult = VecMult(transform.localScale, boxCol.center);
    //    var pos = transform.position + (transform.forward * mult.z + transform.right * mult.x + transform.up * mult.y);

    //    var size = VecMult(transform.localScale, boxCol.size);

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(pos, size);

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, 0.1f);
        
    //}


    public void StartObject()
    {
        count = 0;
        buttonRenderer.material = releasedColor;
        activated = true;
    }

    public void StopObject()
    {
        count = 0;

        activated = false;
    }
}
