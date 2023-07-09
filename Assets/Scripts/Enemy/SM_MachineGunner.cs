using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SM_MachineGunner : EnemyBase, IDamagable, IEntity, IVoidDamage, IApplyVelocity
{

    [Header("----Machine Gunner States----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyChasePlayerState chasePlayerState;
    [SerializeField] EnemyShootState enemyShootState;
    [SerializeField] EnemyDeathState deathState;
    [SerializeField] Transform shootPos;
    [SerializeField] EnemyStunState stunState;
    [SerializeField] EnemyPushedState pushedState;


    [Header("--- other values ---")]
    [SerializeField] float attackRange;
    [SerializeField] float stunTime;
    [SerializeField] float betweenShotTime;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;

    [Header("----Events----")]
    public UnityEvent OnEnemyDeathEvent;

    private bool isDead;
    bool isStunned;
    bool isUnstunned;
    float timeBetweenShots;

    bool hasLanded;
    bool wasPushed;

    bool shootTimer = true;

    void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, chasePlayerState, OnChasePlayer);
        stateMachine.AddTransition(chasePlayerState, idleState, OnIdle);

        stateMachine.AddTransition(chasePlayerState, enemyShootState, OnShoot);
        stateMachine.AddTransition(idleState, enemyShootState, OnShoot);
        stateMachine.AddTransition(enemyShootState, idleState, OnShootStop);
       

        stateMachine.AddAnyTransition(stunState, OnStunned);
        stateMachine.AddTransition(stunState, idleState, OnUnstunned);

        stateMachine.AddAnyTransition(deathState, () => isDead);


        stateMachine.AddAnyTransition(pushedState, OnPushed);
        stateMachine.AddTransition(pushedState, idleState, OnPushLanding);


        enemyColor = enemyMeshRenderer.material.color;
    }

    
    void Update()
    {

        Debug.Log("state: " + stateMachine.CurrentState);
        if (enemyEnabled)
        {
            stateMachine.Tick();

            if (GetDoesSeePlayer())
            {
                RotToPlayer();
            }
            
        }
    }

    bool OnShootStop()
    {
        var x = enemyShootState.ExitCondition();

        if (x)
        {
            StopCoroutine(OnWait());
            StartCoroutine(OnWait());
        }

        return x;
    }

    bool OnIdle()
    {
        bool enabled = enemyEnabled;
        bool inDistance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position) < attackRange;

        return inDistance;
    }

    bool OnChasePlayer()
    {
        bool enabled = enemyEnabled;
        bool inDistance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position) > attackRange;

        return enabled && inDistance;
    }
    bool OnPushLanding()
    {
        var temp = hasLanded;
        hasLanded = false;

        if (temp)
        {
            agent.enabled = true;
            rb.isKinematic = true;
            wasPushed = false;
        }

        return temp;
    }
    bool OnPushed()
    {
        var temp = wasPushed;
        wasPushed = false;
        return temp;
    }

    bool OnShoot()
    {
        bool toAttack = GetDoesSeePlayer();
        return toAttack && shootTimer;
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
        isDead = true;
        OnEnemyDeathEvent?.Invoke();
        GetComponent<Collider>().enabled = false;
        // GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        //Destroy(gameObject);
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
        shootTimer = false;
        yield return new WaitForSeconds(betweenShotTime);
        shootTimer = true;
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

    public void FallIntoTheVoid()
    {
        Destroy(gameObject);
    }

    public void ApplyVelocity(Vector3 velocity)
    {

        wasPushed = true;
        agent.enabled = false;
        rb.isKinematic = false;

        rb.AddForce(velocity, ForceMode.Impulse);



    }

    private void OnCollisionEnter(Collision collision)
    {

        foreach (var cont in collision.contacts)
        {
            var norm = cont.normal;
            if (Vector3.Dot(norm, Vector3.up) > 0.8f)
            {
                hasLanded = true;
                return;
            }
        }

    }

}
