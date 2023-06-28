using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Disrupter : EnemyBase, IDamagable, IEntity
{
    
    private void Start()
    {
        health.FillToMax();

        enemyColor = enemyMeshRenderer.material.color;
    }


    public float GetCurrentHealth()
    {
        return health.CurrentValue;
    }

    IEnumerator FlashDamage()
    {
        enemyMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        enemyMeshRenderer.material.color = enemyColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        IceKinesis ice = new IceKinesis();
        LightningKinesis light = new LightningKinesis();
        pyroBlast fire = new pyroBlast();

        if (other.CompareTag("Player"))
        {
            enemyEnabled = true;

            if (enemyEnabled)
            {
                ice.enabled = false;
                light.enabled = false;
                fire.enabled = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyEnabled = false;
        }
    }

    private void OnEnable()
    {
        health.OnResourceDepleted += OnDeath;
    }

    private void OnDisable()
    {
        health.OnResourceDepleted -= OnDeath;
    }

    public void TakeDamage(float dmg)
    {
        health.Decrease(dmg);
    }

    void OnDeath()
    {
        Destroy(gameObject);
    }

    public void Respawn()
    {
        Destroy(gameObject);
    }
}
