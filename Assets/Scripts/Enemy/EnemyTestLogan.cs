using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestLogan : EnemyBase, IDamagable
{
    [Header("----- Sates -----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyTestMoveStateLogam TestMoveState;

    private void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, TestMoveState, OnMove);
        stateMachine.AddTransition(TestMoveState, idleState, OnIdle);
    }

    bool OnIdle()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerPOS(), gameObject.transform.position);

        bool notInDistance = distance > 10;

        return notInDistance;
    }

    bool OnMove()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerPOS(), gameObject.transform.position);

        bool inDistance = distance < 10;

        return inDistance;
    }

    public void Update()
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
    public void TakeIceDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeElectroDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeLaserDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    IEnumerator FlashDamage()
    {
        enemyColor = Color.white;
        enemyMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        enemyMeshRenderer.material.color = enemyColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enemyEnabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enemyEnabled = false;
        }
    }

    public float GetCurrentHealth()
    {
        return health.CurrentValue;
    }
}
