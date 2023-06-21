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


    Vector3 lastPos;
    int frames;
    void Start()
    {
        Destroy(gameObject, destroyBulletTimer);
        rb.velocity = transform.forward * bulletSpeed;
        lastPos = transform.position;
        frames = 0;
    }



    private void FixedUpdate()
    {
        if (frames == 0)
            return;
        RaycastHit hit;
        bool doesHit = Physics.Raycast(lastPos, transform.forward, out hit);
        if(doesHit)
        {
            if(hit.collider.gameObject != gameObject)
            {
                var dam = hit.collider.GetComponent<IDamagable>();
                if(dam != null)
                {
                    dam.TakeDamage(bulletDamage);
                }
                Destroy(gameObject);
            }
        }

        frames++;
        lastPos = transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;


        IDamagable damagable = other.GetComponent<IDamagable>();

        if (damagable != null)
            damagable.TakeDamage(bulletDamage);

        Destroy(gameObject);
    }




}
