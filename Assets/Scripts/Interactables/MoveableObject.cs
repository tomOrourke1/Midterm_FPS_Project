using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveableObject : MonoBehaviour, ITelekinesis, IEntity
{

    [SerializeField] Rigidbody rb;
    [SerializeField] int damage;
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
        if(thrown && !collision.collider.CompareTag("Player"))
        {
            var iDamage = collision.collider.GetComponent<IDamagable>();
            if(iDamage != null)
            {
                gameObject.GetComponent<ExplodingBarrel>()?.contactExplosion();

                iDamage.TakeDamage(damage);
            }
        }

        thrown = false;
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    public void Respawn()
    {
        Destroy(gameObject);
    }
}
