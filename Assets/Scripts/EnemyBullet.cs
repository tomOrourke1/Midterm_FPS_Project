using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int enBulletDamage;
    [SerializeField] int enBulletspeed;
    [SerializeField] float destroyBulletTimer;

    [SerializeField] Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyBulletTimer);
        rb.velocity = transform.forward * enBulletspeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();

        if (damagable != null)
        {
            damagable.TakeDamage(enBulletDamage);
        }

        Destroy(gameObject);
    }
}
