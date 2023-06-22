using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flanker : EnemyBase, IDamagable, IEntity
{
    [Header("----- Flanker States -----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyFlankState flankState;
    [SerializeField] EnemyToFlankState toFlankState;
    [SerializeField] EnemyShootState shootState;



    [Header("----- Flanker Stats -----")]
    [Range(1, 10)][SerializeField] int playerFaceSpeed;
    [SerializeField] float distToChase;
    [SerializeField] float distToFlank;
    [SerializeField] float distToIdle;

    [Header("--- componenets ---")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform shootPos;


    [Header("- timers -")]
    [SerializeField] float timeBetweenShots;

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
