using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveableObject : MonoBehaviour, ITelekinesis
{

    [SerializeField] Rigidbody rb;


    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Rigidbody GetRigidbody()
    {
        Debug.Log("Get RB");
        return rb;
    }

    public void TakeVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    private void OnValidate()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
    }
}
