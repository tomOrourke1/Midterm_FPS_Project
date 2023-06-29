using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SM_Scientist : EnemyBase, IDamagable, IEntity, IApplyVelocity 
{
    [Header("----- States -----")]
    [SerializeField] EnemyIdleState idleState; // creates Idle state
    [SerializeField] EnemyMoveAwayState runState; // creates Move Away state
    [SerializeField] EnemySpottedPlayer scientistSpotPlayer;
    [SerializeField] EnemyPushedState pushedState;

    [Header("----- Other Vars -----")]
    [SerializeField] float idleRange;


    [Header("Keys")]
    [SerializeField] GameObject key;

    bool wasPushed;
    bool hasLanded;
    [SerializeField] Rigidbody rb;
    [SerializeField] NavMeshAgent agent;

    private void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);



        stateMachine.AddTransition(idleState, runState, OnRun);
        stateMachine.AddTransition(runState, idleState, OnIdle);

        stateMachine.AddAnyTransition(pushedState, OnPushed);
        stateMachine.AddTransition(pushedState, idleState, OnPushLanding);

        rb.isKinematic = false;


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
        Instantiate(key, transform.position, Quaternion.identity);
        Destroy(gameObject); // destroy enemy
    }
    public void ApplyVelocity(Vector3 velocity)
    {
        wasPushed = true;
        agent.enabled = false;
        rb.isKinematic = false;

        rb.AddForce(velocity, ForceMode.Impulse);


    }
    public void TakeDamage(float dmg)
    {
        health.Decrease(dmg); // decrease the enemies hp with the weapon's damage

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
