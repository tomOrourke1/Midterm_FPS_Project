using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Disrupter : EnemyBase, IDamagable, IEntity
{
    public KinesisSelect select;
    [SerializeField] SphereCollider ball;
    [SerializeField] DisruptorField fieldScript;

    bool wasCryoOn, wasPyroOn, wasElectroOn, wasAeroOn;

    [Header("Hit SFX")]
    [SerializeField] EnemyAudio audScript;

    [System.Obsolete]
    private void Start()
    {
        audScript = GetComponent<EnemyAudio>();
        UpdateBools();
        health.FillToMax();
        enemyColor = enemyMeshRenderer.material.color;
        fieldScript.GetParticleSystem().startSize = ball.radius * 2f;
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
            case KinesisSelect.cryokinesis: 
                GameManager.instance.GetEnabledList().CryoSetActive(false); break;
            case KinesisSelect.aerokinesis: 
                GameManager.instance.GetEnabledList().AeroSetActive(false); break;
            case KinesisSelect.electrokinesis: 
                GameManager.instance.GetEnabledList().ElectroSetActive(false); break;
            case KinesisSelect.pyrokinesis: 
                GameManager.instance.GetEnabledList().PyroSetActive(false); break;
        }
           
    }
    private void TurnOnKinesis(KinesisSelect _k)
    {
        switch (_k)
        {
            case KinesisSelect.cryokinesis:
                if (wasCryoOn)
                {
                    GameManager.instance.GetEnabledList().CryoSetActive(true);
                }
                break;
            case KinesisSelect.aerokinesis:
                if (wasAeroOn)
                {
                    GameManager.instance.GetEnabledList().AeroSetActive(true);
                }
                break;
            case KinesisSelect.electrokinesis:
                if (wasElectroOn)
                {
                    GameManager.instance.GetEnabledList().ElectroSetActive(true);
                }
                break;
            case KinesisSelect.pyrokinesis:
                if (wasPyroOn)
                {
                    GameManager.instance.GetEnabledList().PyroSetActive(true);
                }
                break;
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        UpdateBools();
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
        audScript.PlayEnemy_Hurt();
        health.Decrease(dmg);
    }
    public void TakeIceDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeElectroDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeLaserDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    void OnDeath()
    {
        Destroy(gameObject);
    }

    public void Respawn()
    {
        Destroy(gameObject);
    }

    private void UpdateBools()
    {
        wasPyroOn = GameManager.instance.GetEnabledList().PyroEnabled();
        wasCryoOn = GameManager.instance.GetEnabledList().CryoEnabled();
        wasElectroOn = GameManager.instance.GetEnabledList().ElectroEnabled();
        wasAeroOn = GameManager.instance.GetEnabledList().AeroEnabled();
    }
}
