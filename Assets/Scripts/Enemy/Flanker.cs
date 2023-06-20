using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flanker : EnemyBase, IDamagable
{
    [Header("----- Flanker States -----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyFlankState enemyFlankState;
    [SerializeField] EnemyShootState shootState;
    [SerializeField] Transform shootPos;

    [Header("----- Flanker Stats -----")]
    [Range(1, 10)][SerializeField] int playerFaceSpeed;
    [Range(1, 360)][SerializeField] float viewConeAngle;
    [SerializeField] NavMeshAgent agent;

    [SerializeField] float distToChase;


    bool isAttacking;
    bool doesSeePlayer;

    void Start()
    {

        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, enemyFlankState, OnMove);
        stateMachine.AddTransition(enemyFlankState, shootState, OnAttack);
        stateMachine.AddTransition(shootState, enemyFlankState, shootState.ExitCondition);

        stateMachine.AddTransition(enemyFlankState, idleState, OnIdle);

    }

    bool OnAttack()
    {
        bool toAttack = doesSeePlayer;
        return doesSeePlayer;
    }
    bool OnIdle()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance > distToChase;
        return inDistance;
    }

    bool OnMove()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance < distToChase;
        return inDistance;
    }

    

    void facePlayer()
    {
        var dirToPlayer = GameManager.instance.GetPlayerPOS() - transform.position;
        Quaternion rot = Quaternion.LookRotation(new Vector3(dirToPlayer.x, 0, dirToPlayer.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }


    void Update()
    {

        if (enemyEnabled)
        {
            stateMachine.Tick();

            var dirToPlayer = GameManager.instance.GetPlayerPOS() - transform.position;
            var angle = Vector3.Angle(dirToPlayer, gameObject.transform.forward);

            doesSeePlayer = (angle <= viewConeAngle);

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                facePlayer();
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
}
