using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : EnemyBase, IEntity, IDamagable
{
    [Header("----- States -----")]
    [SerializeField] Shield_Idle shieldIdle;
    [SerializeField] Shield_ToPosition shieldMoveToPosition;
    [SerializeField] Shield_HoldPosition shieldHoldPosition;
    [SerializeField] Shield_TooClose shieldPunch;

    [Header("----- Stats -----")]
    [SerializeField] Transform Home;
    [SerializeField] float knockBack;
    [SerializeField] float damage;
    [SerializeField] float Close_RangeMax;
    [SerializeField] float ShieldingDistance_RangeMax;
    [SerializeField] float Far_RangeMax;


    // Might need a search function for when line of sight is broken


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

    private bool outOfRange()
    {
        // After everything else is implemented have the shield enemy return home

        return true;
    }

    void Die()
    {
        Destroy(gameObject);
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

    public void Respawn()
    {

    }
}
