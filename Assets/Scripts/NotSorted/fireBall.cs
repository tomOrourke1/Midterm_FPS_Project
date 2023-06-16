using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour
{

    [SerializeField] float destroyTimer;
    [SerializeField] int damage;
    [SerializeField] float explosionRange;
    [SerializeField] SphereCollider explosionRadius;

    // Start is called before the first frame update
    void Start()
    {
        explosionRange = explosionRadius.radius;
        Destroy(gameObject, destroyTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {

        
        var colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<IDamagable>() != null)
            {
                RaycastHit hit;

                // Gets the direction of the collider from the barrel.
                var dir = collider.transform.position - transform.position;
                //Debug.DrawRay(transform.position, dir);

                // Casts from the barrel to the collider
                Physics.Raycast(transform.position, dir, out hit);

                // Damages the collider if the raycast connected with it.
                if (hit.collider == collider)
                {
                    collider.GetComponent<IDamagable>().TakeDamage(damage);
                }
            }
        }
        Destroy(gameObject);


    }
}
