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

    [Header("-----Brute Stats-----")]
    [SerializeField] int damage;
    [SerializeField] int attackRate;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] int viewConeAngle;
    [SerializeField] float chargeTime;
    [Range(1,10)][SerializeField] int Armor;


    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;

    bool isAttacking;
    bool isCharging;
    void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, chasePlayerState, OnMove);
        //stateMachine.AddTransition(chasePlayerState, enemyShootState, OnAttack);
        stateMachine.AddTransition(chasePlayerState, idleState, OnIdle);
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

    //bool OnAttack()
    //{
    //    bool toAttack = doesSeePlayer;
    //    if (!isAttacking) 
    //    {

    //        isAttacking = true;
    //        yield return new WaitForSeconds(attackRate);
    //    }
       
    //}
    bool OnCharge()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);

        bool inDistance = distance > 5;

        if(inDistance == true)
        {
            isCharging = true;
        }
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
}
