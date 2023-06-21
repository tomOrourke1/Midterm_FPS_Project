using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Sniper : EnemyBase, IDamagable, IEntity
{
    [Header("-----Sniper States-----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemySniperShootState enemyShootState;
    [SerializeField] EnemyMoveAwayState runAway;

    [Header("-----Sniper Stats------")]
    [SerializeField] float betweenShotTime;
    [SerializeField] float runDistance;
    [SerializeField] float rotSpeed;

    // values for the timer
    float timeBetweenShots;


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
}
