using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MachineGunner : EnemyBase , IDamagable, IEntity
{

    [Header("----Machine Gunner States----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyChasePlayerState chasePlayerState;
    [SerializeField] EnemyShootState enemyShootState;
    [SerializeField] Transform shootPos;
    [SerializeField] EnemyStunState stunState;


    [Header("--- other values ---")]
    [SerializeField] float attackRange;
    [SerializeField] float stunTime;
    [SerializeField] float betweenShotTime;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;

    bool isStunned;
    bool isUnstunned;
    float timeBetweenShots;

    void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, chasePlayerState, OnChasePlayer);

        stateMachine.AddTransition(chasePlayerState, enemyShootState, OnShoot);
        stateMachine.AddTransition(enemyShootState, idleState, enemyShootState.ExitCondition);
       

        stateMachine.AddAnyTransition(stunState, OnStunned);
        stateMachine.AddTransition(stunState, idleState, OnUnstunned);
    }

    
    void Update()
    {
        if (enemyEnabled)
        {
            stateMachine.Tick();

            if (GetDoesSeePlayer())
            {
                RotToPlayer();
            }
            
        }
    }

    bool OnChasePlayer()
    {
        bool enabled = enemyEnabled;
        bool inDistance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position) > attackRange;

        return enabled && inDistance;
    }

    bool OnBetweenShots()
    {
        timeBetweenShots += Time.deltaTime;
        if (timeBetweenShots >= betweenShotTime)
        {
            timeBetweenShots = 0;
            return true;
        }


        return false;
    }

    bool OnShoot()
    {
        bool toAttack = OnBetweenShots() && GetDoesSeePlayer();
        return toAttack;
    }

    bool OnStunned()
    {

        var temp = isStunned;
        isStunned = false;
        return temp;
    }

    bool OnUnstunned()
    {

        var temp = isUnstunned;
        isUnstunned = false;
        if (temp)
        {
            isStunned = false;
        }
        return temp;
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
        health.Decrease(dmg);

        SetFacePlayer();

        StartCoroutine(FlashDamage());

    }
    public void TakeIceDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeElectroDamage(float dmg)
    {
        
        TakeDamage(dmg);

        StartCoroutine(StunTimer());
    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg);
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
    IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(stunTime);
        isUnstunned = true;
        isStunned = false;

    }

    IEnumerator OnWait()
    {
        yield return new WaitForSeconds(2);
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

    public void Respawn()
    {
        Destroy(gameObject);
    }
   
}
