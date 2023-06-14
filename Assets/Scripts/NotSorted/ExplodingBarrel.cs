using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : MonoBehaviour, IDamagable, IEntity
{
    [SerializeField] int Durability;
    [SerializeField] float ExplosionTimer;
    [SerializeField] float ExplosionRange;
    [SerializeField] float ExplosionDamage;

    List<IDamagable> victims;

    public void TakeDamage(float dmg)
    {
        Durability--;

        if (Durability <= 0 )
        {
            Timer();
        }
    }

    void Timer()
    {
        // Wait for timer
        Explode();
    }

    void Explode()
    {
        // Physics.SphereOverlay for damage
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRange);


        // Delete Barrel
        Destroy(gameObject);
    }

    public void Respawn()
    {
        Destroy(gameObject);
    }
}
