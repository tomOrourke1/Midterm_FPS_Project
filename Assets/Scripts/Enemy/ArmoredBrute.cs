using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ArmoredBrute : EnemyBase,IDamagable
{
    [Header("-----States-----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyChasePlayerState chasePlayerState;
    [SerializeField] EnemyShootState enemyShootState;
    [SerializeField] EnemyStunState stunState;
    [SerializeField] EnemyChargeState chargeState;

    [Header("-----Brute Stats-----")]
    [SerializeField] int damage;
    [SerializeField] int chargeDamage;
    [SerializeField] int attackRate;
    [SerializeField] int playerFaceSpeed;
   
    [SerializeField] float chargeTime;
    [SerializeField] float stunTime;
    [SerializeField] float force;
    [SerializeField] float upwardForce;
    [SerializeField] float chargeSpeed;
    [Range(1,10)][SerializeField] int Armor;


    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;

    Vector3 forceDirection;


    bool isAttacking;
    bool isCharging;
    bool isUnstunned;
   bool isStunned;
  void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, chasePlayerState, OnMove);

       stateMachine.AddTransition(chargeState, stunState, OnStun);
        stateMachine.AddTransition(chasePlayerState, chargeState, OnCharge);
       stateMachine.AddTransition(chasePlayerState, enemyShootState, OnAttack);
        stateMachine.AddTransition(chasePlayerState, idleState, OnIdle);
        stateMachine.AddTransition(stunState, idleState, OnUnstun);
    }

     bool OnIdle()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance > 10;
        return inDistance;
    }

    bool OnMove()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance < 10;
        return inDistance;
    }

    bool OnAttack()
    {
     bool toAttack = GetDoesSeePlayer();
     if (!isAttacking) 
       {

         isAttacking = true;
         
        }
        return isAttacking;
    }
    bool OnCharge()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);

        bool inDistance = distance > 5;

        if(inDistance == true)
        {
            isCharging = true;
            StartCoroutine(ChargeTimer());
        }
        return isCharging;
    }

    void Update()
    {

        if (enemyEnabled)
        {
            stateMachine.Tick();
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

    void OnDeath()
    {
        Destroy(gameObject);
    }
   bool OnStun()
    {
        return isStunned;
    }
    bool OnUnstun()
    {
        return isUnstunned;
    }
    public void TakeDamage(float dmg)
    {
        health.Decrease(dmg/Armor);
        StartCoroutine(FlashDamage());
    }
    public void TakeIceDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeElectroDamage(float dmg)
    {
        TakeDamage(dmg/Armor);
    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg/Armor);
    }
    public void TakeLaserDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    IEnumerator FlashDamage()
    {
        enemyMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        enemyMeshRenderer.material.color = enemyColor;
    }
    IEnumerator ChargeTimer()
    {
        yield return new WaitForSeconds(chargeTime);
        agent.enabled = false;
        rb.isKinematic = false;
        forceDirection = gameObject.transform.forward;
        Vector3 speed = forceDirection * chargeSpeed;
        rb.AddForce(speed, ForceMode.Force);
    }
    IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(stunTime);
        isUnstunned = true;
        isStunned = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyEnabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyEnabled = false;
        }
    }

    public float GetCurrentHealth()
    {
        return health.CurrentValue;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(isCharging == true)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
                damagable.TakeDamage(chargeDamage);
                var applyVel = collision.gameObject.GetComponent<IApplyVelocity>();
                forceDirection = gameObject.transform.forward;
                Vector3 velocity = forceDirection * force + transform.up * upwardForce;
                applyVel.ApplyVelocity(velocity);
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                agent.enabled = true;
                rb.isKinematic = true;
            }
            else
            {
              isStunned = true;
                isUnstunned = false;
                StartCoroutine(StunTimer());
                agent.enabled = true;
                rb.isKinematic = true;
            }
        }
      isCharging = false;
    }
}
