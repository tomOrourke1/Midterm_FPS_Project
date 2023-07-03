using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BasicEnemy : EnemyBase, IDamagable, IEntity, IApplyVelocity
{
    [Header("---- States ----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyChasePlayerState chasePlayer;
    [SerializeField] EnemyMoveAwayState moveAway;
    [SerializeField] EnemyStrafeState strafeState;
    [SerializeField] EnemyShootState shootState;
    [SerializeField] EnemyPushedState pushedState;
    [SerializeField] EnemyStunState stunState;
    [SerializeField] EnemyDeathState deathState;

    [Header("--- other values ---")]
    [SerializeField] float attackRange;
    [SerializeField] float moveAwayRange;
    [SerializeField] float stunTime;

    [SerializeField] float strafeTime;
     float timeInStrafe;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;

    [Header("----Events----")]
    public UnityEvent OnEnemyDeathEvent;

    private bool isDead;
    bool wasPushed;
    bool hasLanded;
    bool isStunned;
    bool isUnstunned;

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
        stateMachine.AddTransition(idleState, strafeState, OnStrafe);
        stateMachine.AddTransition(idleState, moveAway, OnMoveAway);


        stateMachine.AddTransition(strafeState, shootState, OnShoot);
        stateMachine.AddTransition(shootState, strafeState, shootState.ExitCondition);

        stateMachine.AddAnyTransition(pushedState, OnPushed);
        stateMachine.AddTransition(pushedState, idleState, OnPushLanding);


        stateMachine.AddAnyTransition(stunState, OnStunned);
        stateMachine.AddTransition(stunState, idleState, OnUnstunned);

        rb.isKinematic = false;

        enemyColor = enemyMeshRenderer.material.color;

        stateMachine.AddAnyTransition(deathState, () => isDead);
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
    private void Update()
    {
       // Debug.LogError("Current state: " + stateMachine.CurrentState.ToString());
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

        StartCoroutine(FlashDamage());        
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
        if (stateMachine.CurrentState is EnemyPushedState)
        {
            CheckHasLanded(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (stateMachine.CurrentState is EnemyPushedState)
        {
            CheckHasLanded(collision);
        }
    }

    private void CheckHasLanded(Collision collision)
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
