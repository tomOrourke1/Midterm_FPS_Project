using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Sniper : EnemyBase, IDamagable, IEntity, IApplyVelocity
{
    [Header("-----Sniper States-----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemySniperShootState enemyShootState;
    [SerializeField] EnemyMoveAwayState runAway;
    [SerializeField] EnemyPushedState pushedState;

    [Header("-----Sniper Stats------")]
    [SerializeField] float betweenShotTime;
    [SerializeField] float runDistance;
    [SerializeField] float rotSpeed;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;
    // values for the timer
    float timeBetweenShots;


    bool wasPushed;
    bool hasLanded;

    void Start()
    {
        health.FillToMax();
        enemyColor = enemyMeshRenderer.material.color;
        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);



        stateMachine.AddTransition(idleState, enemyShootState, OnAttack);
        stateMachine.AddTransition(enemyShootState, idleState, enemyShootState.ExitCondition);


        stateMachine.AddTransition(idleState, runAway, OnRunAway);
        stateMachine.AddTransition(runAway, idleState, OnStopRunning);

        stateMachine.AddAnyTransition(pushedState, OnPushed);
        stateMachine.AddTransition(pushedState, idleState, OnPushLanding);

        rb.isKinematic = false;

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


    bool OnRunAway()
    {
        return GetDoesSeePlayer() && (GetDistToPlayer() <= runDistance);
    }
    bool OnStopRunning()
    {
        return (GetDistToPlayer() > runDistance);
    }


    bool OnAttack()
    {
        bool toAttack = OnBetweenShots() && GetDoesSeePlayer();
        return toAttack;
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


    void Update()
    {
        if (enemyEnabled)
        {
            stateMachine.Tick();

            if(GetDoesSeePlayer() )
            {
                RotToPlayer();
            }
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
    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void ApplyVelocity(Vector3 velocity)
    {
        wasPushed = true;
        agent.enabled = false;
        rb.isKinematic = false;

        rb.AddForce(velocity, ForceMode.Impulse);


    }

    IEnumerator FlashDamage()
    {
        enemyMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        enemyMeshRenderer.material.color = enemyColor;
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
