using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SM_SecurityGuard : EnemyBase, IDamagable, IEntity
{
    [Header("----- States ----- ")]
    [SerializeField] EnemyIdleState idleState; // creates Idle state
    [SerializeField] EnemyChasePlayerState followState; // creates Chase Player state
    [SerializeField] EnemyShootState attackState; // creates shoot state
    [SerializeField] EnemySpottedPlayer findPlayerState;
    [SerializeField] EnemyBackToPointState backToPointState;


    [Header("----- Other Vars -----")]
    [SerializeField] float attackRange;
    [SerializeField] float closeToPlayer;


    [SerializeField] NavMeshAgent agent;



    bool wasHit;


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

            if(GetDoesSeePlayer())
            {
                RotToPlayer();
            }
        }
    }




    void OnDeath()
    {
        Destroy(gameObject); // destroy enemy
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
}
