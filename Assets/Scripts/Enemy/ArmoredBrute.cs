using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmoredBrute : EnemyBase,IDamagable
{
    [Header("-----States-----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyChasePlayerState chasePlayerState;
    [SerializeField] EnemyShootState enemyShootState;

    [Header("-----Brute Stats-----")]
    [SerializeField] int damage;
    [SerializeField] int attackRate;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] int viewConeAngle;

    bool isAttacking;

    void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, chasePlayerState, OnMove);
        //stateMachine.AddTransition(chasePlayerState, enemyShootState, OnAttack);
        stateMachine.AddTransition(chasePlayerState, idleState, OnIdle);
    }

     bool OnIdle()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance > 10;
        return inDistance;
    }

    bool OnMove()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);

        bool inDistance = distance < 10;
        return inDistance;
    }

    //bool OnAttack()
    //{
    //    bool toAttack = doesSeePlayer;
    //    if (!isAttacking) 
    //    {

    //        isAttacking = true;
    //        yield return new WaitForSeconds(attackRate);
    //    }
       
    //}


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
