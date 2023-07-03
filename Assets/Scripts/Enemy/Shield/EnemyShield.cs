using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyShield : EnemyBase, IEntity, IDamagable, IApplyVelocity
{
    [Header("----- States -----")]
    [SerializeField] Shield_Idle shieldIdle;
    [SerializeField] Shield_ToPosition shieldMoveToPosition;
    [SerializeField] Shield_HoldPosition shieldHoldPosition;
    [SerializeField] Shield_TooClose shieldPunch;
    [SerializeField] EnemyPushedState pushedState;
    [SerializeField] EnemyDeathState deathState;

    [Header("----- Stats -----")]
    [SerializeField] Transform Home;
    [SerializeField] float knockBack;
    [SerializeField] float damage;
    [SerializeField] float Close_RangeMax;
    [SerializeField] float ShieldingDistance_RangeMax;
    [SerializeField] float Far_RangeMax;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;
    // Might need a search function for when line of sight is broken
    [Header("----Events----")]
    public UnityEvent OnEnemyDeathEvent;

    private bool isDead;
    bool wasPushed;
    bool hasLanded;
    private void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();

        stateMachine.SetState(shieldIdle);

        // Idle
        stateMachine.AddTransition(shieldIdle, shieldPunch, close);
        stateMachine.AddTransition(shieldIdle, shieldHoldPosition, shieldingDistance);
        stateMachine.AddTransition(shieldIdle, shieldMoveToPosition, Far);

        // Move to position
        stateMachine.AddTransition(shieldMoveToPosition, shieldPunch, close);
        stateMachine.AddTransition(shieldMoveToPosition, shieldHoldPosition, shieldingDistance);
        stateMachine.AddTransition(shieldMoveToPosition, shieldIdle, outOfRange);

        // Hold position
        stateMachine.AddTransition(shieldHoldPosition, shieldPunch, close);
        stateMachine.AddTransition(shieldHoldPosition, shieldMoveToPosition, Far);
        stateMachine.AddTransition(shieldHoldPosition, shieldIdle, outOfRange);

        // Melee Attack
        stateMachine.AddTransition(shieldPunch, shieldHoldPosition, shieldingDistance);
        stateMachine.AddTransition(shieldPunch, shieldMoveToPosition, Far);
        stateMachine.AddTransition(shieldPunch, shieldIdle, outOfRange);

        //When hit with AeroKinesis
        stateMachine.AddAnyTransition(pushedState, OnPushed);
        stateMachine.AddTransition(pushedState, shieldIdle, OnPushLanding);

        rb.isKinematic = false;

        enemyColor = enemyMeshRenderer.material.color;
    }

    private void Update()
    {
        if (enemyEnabled)
        {
            stateMachine.Tick();
        }
    }

    private void OnEnable()
    {
        health.OnResourceDepleted += Die;
    }

    private void OnDisable()
    {
        health.OnResourceDepleted -= Die;
    }

    // State functions

    private bool close()
    {
        var distance = Vector3.Distance(gameObject.transform.position, GameManager.instance.GetPlayerPOS());

        // Player distance from enemy < CloseRangeMax
        if (distance < Close_RangeMax)
        {
            return true;
        }

        return false;
    }

    private bool shieldingDistance()
    {
        var distance = Vector3.Distance(gameObject.transform.position, GameManager.instance.GetPlayerPOS());

        // Player distance from enemy < ShieldDistanceRangeMax && > CloseRangeMax
        if (distance < ShieldingDistance_RangeMax && distance > Close_RangeMax)
        {
            return true;
        }

        return true;
    }

    private bool Far()
    {
        var distance = Vector3.Distance(gameObject.transform.position, GameManager.instance.GetPlayerPOS());

        // Player distance from enemy < FarRangeMax && > ShieldDistanceRangeMax
        if (distance > ShieldingDistance_RangeMax)
        {
            return true;
        }

        return true;
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
    private bool outOfRange()
    {
        // After everything else is implemented have the shield enemy return home

        return true;
    }

    void Die()
    {
        isDead = true;
        OnEnemyDeathEvent?.Invoke();
        GetComponent<Collider>().enabled = false;
        // GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        //Destroy(gameObject);
    }

    IEnumerator FlashDamage()
    {
        enemyMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        enemyMeshRenderer.material.color = enemyColor;
    }

    // Within Range
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            enemyEnabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag("Player"))
            //enemyEnabled = false;
    }

    // Interfaces
    public float GetCurrentHealth()
    {
        return health.CurrentValue;
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
    public void ApplyVelocity(Vector3 velocity)
    {
        wasPushed = true;
        agent.enabled = false;
        rb.isKinematic = false;

        rb.AddForce(velocity/2, ForceMode.Impulse);


    }
    public void Respawn()
    {

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
