using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase, IDamagable
{
    [Header("---- States ----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyChasePlayerState chasePlayer;
    [SerializeField] EnemyMoveAwayState moveAway;
    [SerializeField] EnemyStrafeState strafeState;
    [SerializeField] EnemyShootState shootState;

    [Header("--- other values ---")]
    [SerializeField] float attackRange;
    [SerializeField] float moveAwayRange;

    [SerializeField] float strafeTime;
     float timeInStrafe;


    private void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(strafeState, chasePlayer, OnChasePlayer);
        stateMachine.AddTransition(chasePlayer, strafeState, OnStrafe);

        stateMachine.AddTransition(chasePlayer, moveAway, OnMoveAway);
        stateMachine.AddTransition(moveAway, strafeState, OnStrafe);
        stateMachine.AddTransition(strafeState, moveAway, OnMoveAway);

        stateMachine.AddTransition(idleState, chasePlayer, OnChasePlayer);


        stateMachine.AddTransition(strafeState, shootState, OnShoot);
        stateMachine.AddTransition(shootState, strafeState, shootState.ExitCondition);


        enemyColor = enemyMeshRenderer.material.color;
    }

    bool OnShoot()
    {
        timeInStrafe += Time.deltaTime;


        if(timeInStrafe >= strafeTime)
        {
            timeInStrafe = 0;
            return true;
        }
        else
        {
            return false;
        }
    }


    bool OnMoveAway()
    {
        var dist = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position);


        return dist <= moveAwayRange;
    }
    bool OnChasePlayer()
    {
        bool enabled = enemyEnabled;
        bool inDistance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position) > attackRange;

        return enabled && inDistance;
    }
    bool OnStrafe()
    {
        bool enabled = enemyEnabled;
        var dist = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position);
        bool inDistance =  (dist <= attackRange) && (dist > moveAwayRange);
        
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
        health.OnResourceDepleted += Die;    }

    private void OnDisable()
    {
        health.OnResourceDepleted -= Die;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float dmg)
    {
        health.Decrease(dmg);
        Debug.Log("health: " + health.CurrentValue);

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
        if(other.CompareTag("Player"))
            enemyEnabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            enemyEnabled = false;
    }



}
