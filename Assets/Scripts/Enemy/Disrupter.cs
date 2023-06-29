using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Disrupter : EnemyBase, IDamagable, IEntity
{
    public KinesisSelect select;
    
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

    private void TurnOffKinesis(KinesisSelect _k)
    {
        switch (_k)
        {
            case KinesisSelect.cryokinesis: GameManager.instance.GetEnabledList().CryoSetActive(false); break;
            case KinesisSelect.aerokinesis: GameManager.instance.GetEnabledList().AeroSetActive(false); break;
            case KinesisSelect.electrokinesis: GameManager.instance.GetEnabledList().ElectroSetActive(false); break;
            case KinesisSelect.pyrokinesis: GameManager.instance.GetEnabledList().PyroSetActive(false); break;
        }
           
    }
    private void TurnOnKinesis(KinesisSelect _k)
    {
        switch (_k)
        {
            case KinesisSelect.cryokinesis: GameManager.instance.GetEnabledList().CryoSetActive(true); break;
            case KinesisSelect.aerokinesis: GameManager.instance.GetEnabledList().AeroSetActive(true); break;
            case KinesisSelect.electrokinesis: GameManager.instance.GetEnabledList().ElectroSetActive(true); break;
            case KinesisSelect.pyrokinesis: GameManager.instance.GetEnabledList().PyroSetActive(true); break;
        }

    }



    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            enemyEnabled = true;
 
            TurnOffKinesis(select);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyEnabled = false;

            TurnOnKinesis(select);
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