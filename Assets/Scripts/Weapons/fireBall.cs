using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour
{

    [SerializeField] float destroyTimer;
    [SerializeField] int damage;
    [SerializeField] float explosionRange;
    [SerializeField] GameObject explosion;
    [SerializeField] float pushStrength;
    [SerializeField] float upwardForce;

    [SerializeField] PyroSFX sfx;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var point = collision.GetContact(0).point;
        var norm = collision.GetContact(0).normal;
        Instantiate(explosion, transform.position, Quaternion.identity);

        var colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                
            }
            else if (collider.GetComponent<IDamagable>() != null)
            {
                RaycastHit hit;


                var dir = collider.transform.position - transform.position;
                //Debug.DrawRay(transform.position, dir);

                Physics.Raycast(transform.position, dir, out hit);


                if (hit.collider == collider)
                {
                    collider.GetComponent<IDamagable>().TakeDamage(damage);
                }
            }

            if (collider.GetComponent<IApplyVelocity>() != null)
            {
                RaycastHit hit;


                var dir = collider.transform.position - transform.position;
                //Debug.DrawRay(transform.position, dir);

                Physics.Raycast(transform.position, dir, out hit);


                if (hit.collider == collider)
                {
                    KnockBack(collider);
                }
            }
        }
        Destroy(gameObject);


    }

    void KnockBack(Collider victim)
    {
        IApplyVelocity velocityHandler = victim.GetComponent<IApplyVelocity>();

        Vector3 pushDir = Vector3.zero;

        pushDir = victim.transform.position - transform.position;

        pushDir = pushDir.normalized;

        pushDir *= pushStrength;

        pushDir += Vector3.up * upwardForce;

        velocityHandler.ApplyVelocity(pushDir);
    }
}
