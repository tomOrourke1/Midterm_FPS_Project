using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SM_SecurityGuard : EnemyBase, IDamagable, IEntity, IApplyVelocity
{
    [Header("----- States ----- ")]
    [SerializeField] EnemyIdleState idleState; // creates Idle state
    [SerializeField] EnemyChasePlayerState followState; // creates Chase Player state
    [SerializeField] EnemyShootState attackState; // creates shoot state
    [SerializeField] EnemySpottedPlayer findPlayerState;
    [SerializeField] EnemyBackToPointState backToPointState;
    [SerializeField] EnemyPushedState pushedState;
    [SerializeField] EnemyStunState stunState;
    [SerializeField] EnemyDeathState deathState;


    [Header("----- Other Vars -----")]
    [SerializeField] float attackRange;
    [SerializeField] float closeToPlayer;
    [SerializeField] float stunTime;


    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;

    [Header("----Events----")]
    public UnityEvent OnEnemyDeathEvent;

    private bool isDead;
    bool wasHit;
    bool wasPushed;
    bool hasLanded;
    bool isStunned;
    bool isUnstunned;
    private void Start()
    {
        health.FillToMax(); // Makes Security Guards have full health

        stateMachine = new StateMachine(); // creates new instance of state machine
        stateMachine.SetState(idleState); // sets state machine to idle state
        backToPointState.SetDestination(transform.position);

        stateMachine.AddTransition(idleState, followState, OnSeePlayer);

        stateMachine.AddTransition(followState, attackState, OnAttackPlayer);
        stateMachine.AddTransition(attackState, followState, OnAttackEnd);


        stateMachine.AddTransition(backToPointState, followState, OnSeePlayer);
        stateMachine.AddTransition(findPlayerState, followState, OnSeePlayer);


        // find player and such
        stateMachine.AddTransition(idleState, findPlayerState, OnFindPlayer);
        stateMachine.AddTransition(findPlayerState, backToPointState, OnReachedDestination);

        stateMachine.AddTransition(backToPointState, idleState, OnReachedDestination);

        stateMachine.AddAnyTransition(pushedState, OnPushed);
        stateMachine.AddTransition(pushedState, idleState, OnPushLanding);

        stateMachine.AddAnyTransition(stunState, OnStunned);
        stateMachine.AddTransition(stunState, idleState, OnUnstunned);

        rb.isKinematic = false;

        stateMachine.AddAnyTransition(deathState, () => isDead);
    }

    bool OnAttackEnd()
    {
        return attackState.ExitCondition();
    }

    bool OnAttackPlayer()
    {
        return GetDistToPlayer() <= attackRange;
    }


    bool OnSeePlayer()
    {
        return GetDoesSeePlayer();
    }

    bool OnReachedDestination()
    {
        return agent.remainingDistance <= 0.1f;
    }
    bool OnPushLanding()
    {
        var temp = hasLanded;
        hasLanded = false;

        if(temp)
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
    bool OnFindPlayer()
    {
        var b = wasHit;
        wasHit = false;

        return b;
    }

    public void Update()
    {
        if(enemyEnabled)
        {
            stateMachine.Tick();

            if(GetDoesSeePlayer() && !isDead)
            {
                RotToPlayer();
            }
        }
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
        health.Decrease(dmg); // decrease the enemies hp with the weapon's damage

        //SetFacePlayer();

        // change to state for finding the player
        wasHit = true;

        StartCoroutine(FlashDamage()); // has the enemy flash red when taking damage
    }
    public void TakeIceDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeElectroDamage(float dmg)
    {
        isStunned = true;
        TakeDamage(dmg);

        StartCoroutine(StunTimer());
    }
    IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(stunTime);
        isUnstunned = true;
        isStunned = false;

    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeLaserDamage(float dmg)
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
        enemyColor = enemyMeshRenderer.material.color; // saves enemy's color
        enemyMeshRenderer.material.color = Color.red; // sets enemy's color to red to show damage
        yield return new WaitForSeconds(0.15f); // waits for a few seconds for the player to notice
        enemyMeshRenderer.material.color = enemyColor; // changes enemy's color back to their previous color
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) // checks if the "Player" tag has entered the enemie's range
        {
            enemyEnabled = true; // turns the enemy on
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // checks if the "Player" tag has exited the enemie's range
        {
            enemyEnabled = false; // turns the enemy off
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
    IEnumerator PushedDelay()
    {
        yield return new WaitForSeconds(.5f);
    }



    private void OnCollisionEnter(Collision collision)
    {

        foreach(var cont in collision.contacts)
        {
            var norm = cont.normal;
            if(Vector3.Dot(norm, Vector3.up) > 0.8f)
            {
                hasLanded = true;
                return;
            }
        }

    }

}
