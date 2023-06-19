using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestTom : EnemyBase, IDamagable
{
    [Header("---- States -----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyTestMoveStateTom testMoveState;


    private void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, testMoveState, OnMove);
        
    }

    bool OnMove()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);

        bool inDIstance = distance < 10;


        return inDIstance;
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
}
