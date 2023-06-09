using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    [SerializeField] float bulletDamage;
    [SerializeField] int bulletSpeed;
    [SerializeField] float destroyBulletTimer;

    [SerializeField] Rigidbody rb;

    void Start()
    {
        Destroy(gameObject, destroyBulletTimer);
        rb.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();

        if (damagable != null)
        {
            damagable.TakeDamage(bulletDamage);
        }

        Destroy(gameObject);
    }
}
