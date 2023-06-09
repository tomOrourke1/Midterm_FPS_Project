using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerBullet : MonoBehaviour
{
    [Header("---- Bullet Settings ----")]

    [SerializeField] int damage;
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
        IDamagable damageable = other.GetComponent<IDamagable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}
