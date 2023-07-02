using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flanker : EnemyBase, IDamagable, IEntity, IApplyVelocity
{
    [Header("----- Flanker States -----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyFlankState flankState;
    [SerializeField] EnemyToFlankState toFlankState;
    [SerializeField] EnemyShootState shootState;
    [SerializeField] EnemyPushedState pushedState;
    [SerializeField] EnemyStunState stunState;


    [Header("----- Flanker Stats -----")]
    [Range(1, 10)][SerializeField] int playerFaceSpeed;
    [SerializeField] float distToChase;
    [SerializeField] float distToFlank;
    [SerializeField] float distToIdle;

    [Header("--- componenets ---")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform shootPos;
    [SerializeField] Rigidbody rb;


    [Header("- timers -")]
    [SerializeField] float timeBetweenShots;
    [SerializeField] float stunTime;

    bool wasPushed;
    bool hasLanded;
    bool isStunned;
    bool isUnstunned;
    float time;

    void Start()
    {

        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);


        // idle to Half to flank )
        stateMachine.AddTransition(idleState, toFlankState, OnToFlank);

        stateMachine.AddTransition(toFlankState, flankState, OnFlank);
        stateMachine.AddTransition(flankState, toFlankState, OnToFlankFromFlank);

        stateMachine.AddTransition(flankState, shootState, OnAttack);
        stateMachine.AddTransition(shootState, flankState, shootState.ExitCondition);
        //  half flank to flank
        // flank to half flank


        // flank to attack


        stateMachine.AddAnyTransition(idleState, OnIdle);


        enemyColor = enemyMeshRenderer.material.color;

        stateMachine.AddAnyTransition(pushedState, OnPushed);
        stateMachine.AddTransition(pushedState, idleState, OnPushLanding);

        stateMachine.AddAnyTransition(stunState, OnStunned);
        stateMachine.AddTransition(stunState, idleState, OnUnstunned);

        rb.isKinematic = false;
    }

    bool OnToFlank()
    {
        return GetDoesSeePlayer() && GetDistToPlayer() <= distToChase;
    }
    bool OnToFlankFromFlank()
    {

        var point = flankState.GetBehindPlayer();

        var dist = Vector3.Distance(point, transform.position);


        return dist >= distToFlank;
    }
    bool OnFlank()
    {
        bool val = toFlankState.ExitCondition();



        return val;
    }

    bool OnAttack()
    {
        time += Time.deltaTime;

        if(time >=  timeBetweenShots)
        {
            time = 0;
            return true;
        }

        return false;
    }

    bool OnIdle()
    {
        return GetDistToPlayer() >= distToIdle;
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
    void Update()
    {

        if (enemyEnabled)
        {
            stateMachine.Tick();

            if (GetDoesSeePlayer())
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
