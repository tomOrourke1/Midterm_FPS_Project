using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SM_SecurityGuard : EnemyBase, IDamagable
{
    [Header("----- States ----- ")]
    [SerializeField] EnemyIdleState securityIdle; // creates Idle state
    [SerializeField] EnemyChasePlayerState securityChasePlayer; // creates Chase Player state
    [SerializeField] EnemyShootState securityShoot; // creates shoot state
    [SerializeField] EnemySpottedPlayer securitySpotPlayer;

    [Header("----- Other Vars -----")]
    [SerializeField] float attackRange;
    [SerializeField] float viewConeAngle;
    [SerializeField] float closeToPlayer;
    private bool doesSeePlayer;
    private bool hasBeenHit;
    [SerializeField] NavMeshAgent agent;

    private void Start()
    {
        health.FillToMax(); // Makes Security Guards have full health

        stateMachine = new StateMachine(); // creates new instance of state machine
        stateMachine.SetState(securityIdle); // sets state machine to idle state

        // adds transition from Idle state to Chase State
        stateMachine.AddTransition(securityIdle, securityChasePlayer, OnChasePlayer);
        // adds transition from Chase state to Idle State
        stateMachine.AddTransition(securityChasePlayer, securityIdle, OnIdle);

        stateMachine.AddTransition(securityIdle, securityShoot, SeePlayer);

        stateMachine.AddTransition(securityChasePlayer, securityShoot, ToAttackPlayer);

        stateMachine.AddTransition(securityShoot, securityIdle, securityShoot.ExitCondition);

        stateMachine.AddTransition(securityIdle, securitySpotPlayer, SecurityTakeDam);

        stateMachine.AddTransition(securitySpotPlayer, securityIdle, ReachedDestination);

        stateMachine.AddTransition(securitySpotPlayer, securityChasePlayer, SeePlayer);
    }

    public void Update()
    {
        if(enemyEnabled)
        {
            stateMachine.Tick();

            var angle = Vector3.Angle(GameManager.instance.GetPlayerPOS() - transform.position, gameObject.transform.forward);

            doesSeePlayer = (angle <= viewConeAngle);
        }
    }

    bool ReachedDestination()
    {
        return agent.remainingDistance <= 0.1f;
    }

    bool SecurityTakeDam()
    {
        var temp = hasBeenHit;
        hasBeenHit = false;
        return temp;
    }

    bool SeePlayer()
    {
        return doesSeePlayer;
    }

    bool OnIdle()
    {
        // creates float variable that measure the distance between enemy and player
        float distance = Vector3.Distance(GameManager.instance.GetPlayerPOS(), gameObject.transform.position);

        bool notInDistance = distance > attackRange; // checks if enemy is not in distance of player

        return notInDistance || !doesSeePlayer; // returns true or false depending on notInDistance
    }

    bool ToAttackPlayer()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerPOS(), gameObject.transform.position);

        bool isCloseToPlayer = distance < closeToPlayer;

        return isCloseToPlayer && doesSeePlayer;
    }

    bool OnChasePlayer()
    {
        bool enabled = enemyEnabled;
        bool inDistance = Vector3.Distance(GameManager.instance.GetPlayerPOS(), transform.position) > attackRange;

        return enabled && inDistance && doesSeePlayer;
    }

    void OnDeath()
    {
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

        hasBeenHit = true;
        
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
        if(other.CompareTag("Player")) // checks if the "Player" tag has entered the enemie's range
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
}