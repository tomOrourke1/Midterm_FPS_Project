using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : MonoBehaviour, IDamagable, IEntity
{
    [SerializeField] ParticleSystem ignitionFX;
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] ExplosiveBarrelSFX sfxScript;
    [SerializeField] GameObject explosionParticles;
    [SerializeField] int durability;
    [SerializeField] float timer;
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] float pushStrength;
    [SerializeField] float upwardForce;



    IEnumerator Timer()
    {
        ignitionFX.Play();
        // Wait for timer
        yield return new WaitForSeconds(timer);
        Explode();
    }

    void Explode()
    {
        // Gets everything within range
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        explosionFX.Play();
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
                    collider.GetComponent<IDamagable>().TakeDamage(damage); 

                    collider.gameObject.GetComponent<ExplodingBarrel>()?.chainBlast();

                }

            }
            
            if (collider.GetComponent<IApplyVelocity>() != null)
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
                    KnockBack(collider);

                }
            }
        }

        // Play Explosion effects
        sfxScript.Play_Explosion();
        Effects();

        // Delete Barrel
        Destroy(gameObject);
    }

    void Effects()
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
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

    public void TakeDamage(float dmg)
    {
        if (dmg > 0)
        {
            durability--;

            if (durability <= 0 )
            {
                StartCoroutine(Timer());
            }
        }
    }
    public void TakeIceDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeElectroDamage(float dmg)
    {
      
    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeLaserDamage(float dmg)
    {
        
    }
    public void chainBlast()
    {
        durability = 0;
        TakeDamage(1);
    }

    public void contactExplosion()
    {
        Explode();
    }

    public void Respawn()
    {
        Destroy(gameObject);
    }

    public float GetCurrentHealth()
    {
        return durability;
    }
}
