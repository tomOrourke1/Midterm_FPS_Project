using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : EnemyBase, IDamagable
{
    [Header("-----Sniper States-----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyAttackState attackState;

    [Header("-----Sniper Stats------")]
    [SerializeField] int damage;
    [SerializeField] int shootRate;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] int viewConeAngle;

    
    bool isAttacking;

    void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);
        stateMachine.SetState(attackState);

        stateMachine.AddTransition(idleState, attackState, OnAttack);
        stateMachine.AddTransition(attackState, idleState, OnIdle);
    }

    bool OnAttack()
    {
        if (!isAttacking)
        {
            StartCoroutine(shoot());
        }
        return true;
    }

    bool OnIdle()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance > 20;
        return inDistance;
    }

  
    void Update()
    {
        if (enemyEnabled)
        {
            stateMachine.Tick();
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

    IEnumerator shoot()
    {
        isAttacking = true;
        
        yield return new WaitForSeconds(shootRate);
        isAttacking = false;
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

}
