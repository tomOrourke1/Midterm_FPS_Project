using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase, IDamagable
{
    [Header("---- States ----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyChasePlayerState chasePlayer;
    [SerializeField] EnemyMoveAwayState moveAway;

    [Header("--- other values ---")]
    [SerializeField] float followRange;
    [SerializeField] float attackRange;
    [SerializeField] float moveAwayRange;


    private void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, chasePlayer, OnChasePlayer);
        stateMachine.AddTransition(chasePlayer, idleState, OnIdle);

        stateMachine.AddTransition(chasePlayer, moveAway, OnMoveAway);
        stateMachine.AddTransition(moveAway, idleState, OnIdle);



    }


    bool OnMoveAway()
    {
        var dist = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position);


        return dist < moveAwayRange;
    }
    bool OnChasePlayer()
    {
        bool enabled = enemyEnabled;
        bool inDistance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position) < followRange;

        return enabled && inDistance;
    }
    bool OnIdle()
    {
        bool enabled = enemyEnabled;
        bool inDistance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position) > attackRange;
        
        return enabled && inDistance;
    }




    private void Update()
    {
        if(enemyEnabled)
        {
            stateMachine.Tick();
        }
    }



    private void OnEnable()
    {
        health.OnResourceDepleted += Die;
    }

    private void OnDisable()
    {
        health.OnResourceDepleted -= Die;
    }

    void Die()
    {

    }

    public void TakeDamage(float dmg)
    {
        health.Decrease(dmg);

    }





    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            enemyEnabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            enemyEnabled = false;
    }



}
