using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : EnemyBase, IDamagable, IEntity, IApplyVelocity
{
    [Header("---- States ----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyChasePlayerState chasePlayer;
    [SerializeField] EnemyMoveAwayState moveAway;
    [SerializeField] EnemyStrafeState strafeState;
    [SerializeField] EnemyShootState shootState;
    [SerializeField] EnemyPushedState pushedState;

    [Header("--- other values ---")]
    [SerializeField] float attackRange;
    [SerializeField] float moveAwayRange;

    [SerializeField] float strafeTime;
     float timeInStrafe;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;

    bool wasPushed;
    bool hasLanded;


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

        stateMachine.AddAnyTransition(pushedState, OnPushed);
        stateMachine.AddTransition(pushedState, idleState, OnPushLanding);

        rb.isKinematic = false;

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

        StartCoroutine(FlashDamage());        
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
        if(other.CompareTag("Player"))
            enemyEnabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            enemyEnabled = false;
    }

    public float GetCurrentHealth()
    {
        return health.CurrentValue;
    }

    public void Respawn()
    {
        Destroy(gameObject); ;
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
