using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveableObject : MonoBehaviour, ITelekinesis
{

    [SerializeField] Rigidbody rb;

    bool thrown;

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public void TakeVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
        thrown = true;
    }

    private void OnValidate()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(thrown)
        {
            var iDamage = collision.collider.GetComponent<IDamagable>();
            if(iDamage != null)
            {
                // calculate the daamge based off of velocity thrown?
                int damage = 10; 
                iDamage.TakeDamage(damage);
            }
        }
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
