using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pyroBlast : MonoBehaviour
{
    [Header("------ Fireball Settings ------")]
    [SerializeField] int damage;
    [SerializeField] int splashRange;
    [SerializeField] int speed;
    [SerializeField] float destroyTimer;

    [SerializeField] Rigidbody rb;
    

    
    void Start()
    {
        Destroy(gameObject, destroyTimer);
        rb.velocity = transform.forward * speed;
        
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (splashRange > 0)
        {
            var colliders = Physics.OverlapSphere(transform.position, splashRange);
            foreach(var collider in colliders)
            {
               
            }

        }
        else
        {
            IDamagable damageable = other.GetComponent<IDamagable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
        
    }
    
}
