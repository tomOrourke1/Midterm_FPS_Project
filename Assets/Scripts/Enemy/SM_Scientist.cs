using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SM_Scientist : EnemyBase, IDamagable, IEntity, IApplyVelocity, IVoidDamage
{
    [Header("----- States -----")]
    [SerializeField] EnemyIdleState idleState; // creates Idle state
    [SerializeField] EnemyMoveAwayState runState; // creates Move Away state
    [SerializeField] EnemySpottedPlayer scientistSpotPlayer;
    [SerializeField] EnemyPushedState pushedState;
    [SerializeField] EnemyStunState stunState;
    [SerializeField] EnemyDeathState deathState;

    [Header("----- Other Vars -----")]
    [SerializeField] float idleRange;
    [SerializeField] float stunTime;


    [Header("Keys")]
    [SerializeField] GameObject key;

    private bool isDead;
    bool wasPushed;
    bool hasLanded;
    bool isStunned;
    bool isUnstunned;
    [SerializeField] Rigidbody rb;
    [SerializeField] NavMeshAgent agent;

    [Header("----Events----")]
    public UnityEvent OnEnemyDeathEvent;



    private void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);



        stateMachine.AddTransition(idleState, runState, OnRun);
        stateMachine.AddTransition(runState, idleState, OnIdle);

        stateMachine.AddAnyTransition(pushedState, OnPushed);
        stateMachine.AddTransition(pushedState, idleState, OnPushLanding);

        stateMachine.AddAnyTransition(stunState, OnStunned);
        stateMachine.AddTransition(stunState, idleState, OnUnstunned);

        rb.isKinematic = false;

        stateMachine.AddAnyTransition(deathState, () => isDead);
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
            
            if(GetDoesSeePlayer() && !isDead && isUnstunned == true)
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
    bool OnStunned() 
    {
     
        var temp = isStunned;
        isStunned = false;
        return temp;
    }
    bool OnUnstunned() 
    {
        
        var temp = isUnstunned;
        isUnstunned = false;
        if (temp) 
        {
            isStunned = false;
        }
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
        isDead = true;
        OnEnemyDeathEvent?.Invoke();
        GetComponent<Collider>().enabled = false;
        // GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        //Destroy(gameObject);
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
        isStunned = true;
        TakeDamage(dmg);
      
        StopCoroutine(StunTimer());
        StartCoroutine(StunTimer());
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
        enemyColor = enemyMeshRenderer.material.color; // saves enemy's color
        enemyMeshRenderer.material.color = Color.red; // sets enemy's color to red to show damage
        yield return new WaitForSeconds(0.15f); // waits for a few seconds for the player to notice
        enemyMeshRenderer.material.color = enemyColor; // changes enemy's color back to their previous color
    }
    IEnumerator StunTimer()
    {

        yield return new WaitForSeconds(stunTime);
        isUnstunned = true;
        isStunned = false;

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

    public void FallIntoTheVoid()
    {
        Destroy(gameObject);
    }
}
