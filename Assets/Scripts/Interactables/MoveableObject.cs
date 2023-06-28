using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveableObject : MonoBehaviour, /*ITelekinesis,*/ IEntity, IApplyVelocity
{

    [SerializeField] Rigidbody rb;
    [SerializeField] int damage;

    // Volitile represents whether or not the object deals damage when touched
    bool volitile = false;
    bool initialEnable;

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
        //volitile = true;
    }

    private void OnValidate()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
    }

    //private void Update()
    //{
    //    if (volitile)
    //    {
    //        if (rb.velocity.magnitude <= 0)
    //        {
    //            //volitile = false;
    //        }
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.collider.CompareTag("Player") && volitile)
        {
            var iDamage = collision.collider.GetComponent<IDamagable>();
            if(iDamage != null)
            {
                // Explode the thrown barrels
                rb.gameObject.GetComponent<ExplodingBarrel>()?.contactExplosion();
                iDamage.TakeDamage(damage);
                volitile = false;
            }
        }
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    public void Respawn()
    {
        Destroy(gameObject);
    }
    public void ApplyVelocity(Vector3 velocity) 
    {
        rb.AddForce(velocity, ForceMode.Impulse);
        volitile = true;
    }

    public void SetVolitile(bool newVolitile)
    {
        volitile = newVolitile;
    }

    public bool GetVolitile()
    {
        return volitile;
    }
}
