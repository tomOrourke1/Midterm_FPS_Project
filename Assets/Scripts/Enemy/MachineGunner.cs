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
    [SerializeField] EnemyStunState enemyStunState;


    [Header("--- other values ---")]
    [SerializeField] float attackRange;
    [SerializeField] float stunTime;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;

    bool isStunned;
    bool isUnstunned;

    void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);


    }

    
    void Update()
    {
        
    }
}
