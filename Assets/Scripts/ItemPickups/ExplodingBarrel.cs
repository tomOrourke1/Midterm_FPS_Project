using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : MonoBehaviour, IDamagable, IEntity
{
    [SerializeField] ParticleSystem Fuse;
    [SerializeField] int Durability;
    [SerializeField] float ExplosionTimer;
    [SerializeField] float ExplosionRange;
    [SerializeField] float ExplosionDamage;

    public void TakeDamage(float dmg)
    {
        Durability--;

        if (Durability <= 0 )
        {
            StartCoroutine(Timer());
        }
    }

    public void chainBlast()
    {
        StartCoroutine(Timer());
    }

    public void contactExplosion()
    {
        Explode();
    }

    IEnumerator Timer()
    {
        Fuse.Play();
        // Wait for timer
        yield return new WaitForSeconds(ExplosionTimer);
        Explode();
    }

    void Explode()
    {
        // Gets everything within range
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRange);

        // Grabs everything that was hit and checks if they are damagable
        foreach (Collider collider in colliders)
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
                    collider.GetComponent<IDamagable>().TakeDamage(ExplosionDamage);
                }
            }
        }

        // Play Explosion effects
        Effects();

        // Delete Barrel
        Destroy(gameObject);
    }

    public void Respawn()
    {
        Destroy(gameObject);
    }

    void Effects()
    {

    }

    public float GetCurrentHealth()
    {
        return Durability;
    }
}
