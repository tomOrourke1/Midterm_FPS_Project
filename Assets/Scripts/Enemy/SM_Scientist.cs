using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SM_Scientist : EnemyBase, IDamagable, IEntity
{
    [Header("----- States -----")]
    [SerializeField] EnemyIdleState idleState; // creates Idle state
    [SerializeField] EnemyMoveAwayState runState; // creates Move Away state
    [SerializeField] EnemySpottedPlayer scientistSpotPlayer;

    [Header("----- Other Vars -----")]
    [SerializeField] float idleRange;


    [Header("Keys")]
    [SerializeField] GameObject key;

    private void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);



        stateMachine.AddTransition(idleState, runState, OnRun);
        stateMachine.AddTransition(runState, idleState, OnIdle);



    }

    bool OnIdle()
    {
        return GetDistToPlayer() >= idleRange;
    }

    bool OnRun()
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
        Instantiate(key, transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject); // destroy enemy
    }

    public void TakeDamage(float dmg)
    {
        health.Decrease(dmg); // decrease the enemies hp with the weapon's damage

        SetFacePlayer();

        StartCoroutine(FlashDamage()); // has the enemy flash red when taking damage
    }

    IEnumerator FlashDamage()
    {
        enemyColor = enemyMeshRenderer.material.color; // saves enemy's color
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
