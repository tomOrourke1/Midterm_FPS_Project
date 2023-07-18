using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SM_Turret : EnemyBase, IDamagable, IEntity
{
    [Header("----- States -----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyShootState shootState;

    [Header("--- Other Components ----")]
    [SerializeField] Transform shootPos;
    [Header("----- Other Vars -----")]
    [SerializeField] float attackRange;

    [Header("Hit SFX")]
    [SerializeField] EnemyAudio audScript;

    private void Start()
    {
        audScript = GetComponent<EnemyAudio>();
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, shootState, OnShoot);
        stateMachine.AddTransition(shootState, idleState, OnIdle);

        enemyColor = enemyMeshRenderer.material.color;
    }

    bool OnIdle()
    {
        bool v = shootState.ExitCondition();

        return v || !GetDoesSeePlayer();
    }

    bool OnShoot()
    {
        return GetDoesSeePlayer();
    }





    private void Update()
    {
        if (enemyEnabled)
        {
            stateMachine.Tick();

            if(GetDoesSeePlayer())
            {
                RotToPlayer();
            }

        }
    }



    void OnDeath()
    {
        audScript.PlayEnemy_Death();
        StopAllCoroutines();
        enemyMeshRenderer.material.color = enemyColor;
        Destroy(gameObject); // destroy enemy
    }

    private void OnEnable()
    {
        health.OnResourceDepleted += OnDeath;
    }

    private void OnDisable()
    {
        health.OnResourceDepleted -= OnDeath;
    }

    public void TakeDamage(float dmg)
    {
        health.Decrease(dmg); // decrease the enemies hp with the weapon's damage

        audScript.PlayEnemy_Hurt();

        SetFacePlayer();

        StartCoroutine(FlashDamage()); // has the enemy flash red when taking damage
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
       // enemyColor = enemyMeshRenderer.material.color; // saves enemy's color
        enemyMeshRenderer.material.color = Color.red; // sets enemy's color to red to show damage
        yield return new WaitForSeconds(0.15f); // waits for a few seconds for the player to notice
        enemyMeshRenderer.material.color = enemyColor; // changes enemy's color back to their previous color
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // checks if the "Player" tag has entered the enemie's range
        {
            enemyEnabled = true; // turns the enemy on
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // checks if the "Player" tag has exited the enemie's range
        {
            enemyEnabled = false; // turns the enemy off
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
