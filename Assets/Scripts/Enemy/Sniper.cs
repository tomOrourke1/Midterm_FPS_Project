using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Sniper : EnemyBase, IDamagable
{
    [Header("-----Sniper States-----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyShootState enemyShootState;
    [SerializeField] Transform shootPos;
    

    [Header("-----Sniper Stats------")]
    [SerializeField] int attackRate;
    [Range(1,10)][SerializeField] int playerFaceSpeed;
    [Range(1, 360)][SerializeField] float viewConeAngle;
    [SerializeField] LineRenderer sightLine;
    [SerializeField] NavMeshAgent agent;

    bool isAttacking;
    bool doesSeePlayer;
    

    void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, enemyShootState, OnAttack);
        stateMachine.AddTransition(enemyShootState, idleState, enemyShootState.ExitCondition);
        stateMachine.AddTransition(enemyShootState, idleState, LineBandaid);
    }

    bool LineBandaid()
    {
        if (doesSeePlayer)
        {
            sightLine.SetPosition(0, shootPos.position);
            sightLine.SetPosition(1, GameManager.instance.GetPlayerPOS());
        }
        else
        {
            sightLine.SetPosition(0, Vector3.zero);
            sightLine.SetPosition(1, Vector3.zero);

        }

        return false;
    }

    bool OnAttack()
    {
        bool toAttack = doesSeePlayer;

        return toAttack;
    }

    bool OnIdle()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance > 20;
        return inDistance;
    }

    void facePlayer()
    {
        var dirToPlayer = GameManager.instance.GetPlayerPOS() - transform.position;
        Quaternion rot = Quaternion.LookRotation(new Vector3 (dirToPlayer.x, 0, dirToPlayer.z));
        transform.rotation = Quaternion.Lerp(transform.rotation,rot, Time.deltaTime * playerFaceSpeed);
    }

  
    void Update()
    {
        if (enemyEnabled)
        {
            stateMachine.Tick();

            var dirToPlayer = GameManager.instance.GetPlayerPOS() - transform.position;
            var angle = Vector3.Angle(dirToPlayer, gameObject.transform.forward);

            doesSeePlayer = (angle <= viewConeAngle);

            
            if(agent.remainingDistance <= agent.stoppingDistance)
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
