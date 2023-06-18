using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Scientist : EnemyBase, IDamagable
{
    [Header("----- States -----")]
    [SerializeField] EnemyIdleState scientistIdle; // creates Idle state
    [SerializeField] EnemyMoveAwayState scientistMoveAway; // creates Move Away state
    [SerializeField] EnemySpottedPlayer scientistSpotPlayer;

    [Header("----- Other Vars -----")]
    [SerializeField] float scientistRange;
    [SerializeField] float viewConeAngle;
    private bool doesSeePlayer;
    private bool hasBeenHit;

    private void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(scientistIdle);

        stateMachine.AddTransition(scientistIdle, scientistMoveAway, SeePlayer);

        stateMachine.AddTransition(scientistMoveAway, scientistIdle, OnIdle);

        stateMachine.AddTransition(scientistIdle, scientistMoveAway, ScientistTakeDamage);
    }

    private void Update()
    {
        if (enemyEnabled)
        {
            stateMachine.Tick();

            var angle = Vector3.Angle(GameManager.instance.GetPlayerPOS() - transform.position, gameObject.transform.forward);

            doesSeePlayer = (angle <= viewConeAngle);
        }

    }

    bool ScientistTakeDamage()
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
        float distance = Vector3.Distance(GameManager.instance.GetPlayerPOS(), gameObject.transform.position);

        bool notInDistance = distance > scientistRange;

        return notInDistance;
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
        Destroy(gameObject); // destroy enemy
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
}
