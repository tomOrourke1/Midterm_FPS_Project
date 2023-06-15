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
            IDamagable damageable = collider.GetComponent<IDamagable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
        Destroy(gameObject);


    }
}
