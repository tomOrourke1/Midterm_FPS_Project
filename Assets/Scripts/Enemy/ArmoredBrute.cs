using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ArmoredBrute : EnemyBase, IDamagable, IEntity, IVoidDamage
{
    [Header("-----States-----")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyChasePlayerState chasePlayerState;
    [SerializeField] EnemyMeleeState meleeState;
    [SerializeField] EnemyStunState stunState;
    [SerializeField] EnemyChargeState chargeState;
    [SerializeField] EnemyChargePrepState chargePrepState;
    [SerializeField] EnemyDeathState deathState;

    [Header("-----Brute Stats-----")]
    [SerializeField] int damage;
   
    [SerializeField] int attackRate;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] float stunTime;

    [SerializeField] float moveDistance;
    [SerializeField] float chargeDistance;
    [SerializeField] float attackDistance;
    [Range(1, 10)][SerializeField] int Armor;


    [SerializeField] NavMeshAgent agent;
    Rigidbody rb;



    float beginningStunTime;
    public UnityEvent OnEnemyDeathEvent;
    bool isAttacking;
    bool startChargePrep;
    bool timerOn;
   
    private bool isDead;
    void Start()
    {
        health.FillToMax();

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, chasePlayerState, OnMove);
        stateMachine.AddTransition(chasePlayerState, idleState, OnIdle);

        stateMachine.AddTransition(chasePlayerState, meleeState, OnAttack);
        stateMachine.AddTransition(meleeState, idleState, meleeState.ExitCondition);

        stateMachine.AddTransition(chasePlayerState, chargePrepState, OnChargePrep);
        stateMachine.AddTransition(chargePrepState, chargeState, chargePrepState.ExitCondition);

        stateMachine.AddTransition(chargeState, stunState, OnStun);
        stateMachine.AddTransition(stunState, idleState, OnUnstun);

        stateMachine.AddAnyTransition(deathState, () => isDead);

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (enemyEnabled)
        {
            stateMachine.Tick();

            if (GetDoesSeePlayer() && chargePrepState.isCharging == false && chargeState.isStunned == false && startChargePrep == false)
            {
                RotToPlayer();
            }
        }
    }
    bool OnIdle()

    {
        Debug.Log("on idle");
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance > moveDistance;
        return inDistance;
    }

    bool OnMove()
    {
        Debug.Log("on move");
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance < moveDistance;


        if (distance < moveDistance && distance > chargeDistance && GetDoesSeePlayer())
        {
            if (timerOn == false) { StartCoroutine(Timer()); }
            StartCoroutine(Timer());
            //RotToPlayer();
            startChargePrep = true;
        }
        else if (distance < attackDistance && GetDoesSeePlayer())
        {
            if (timerOn == false) { StartCoroutine(Timer()); }

            isAttacking = true;
        }
        return inDistance;
    }

    bool OnAttack()
    {
        Debug.Log("on attack");
     
        return isAttacking;
    }
    bool OnChargePrep()
    {
        Debug.Log("on charge prep");
        return startChargePrep;
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
        isDead = true;
        OnEnemyDeathEvent?.Invoke();
        GetComponent<Collider>().enabled = false;
        // GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    bool OnStun()
    {
        Debug.Log("on stun");
       
        return chargeState.isStunned;
    }
    bool OnUnstun()
    {
        return chargeState.isUnstunned;
    }
    public void TakeDamage(float dmg)
    {
        health.Decrease(dmg / Armor);
        StartCoroutine(FlashDamage());
    }
    public void TakeIceDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeElectroDamage(float dmg)
    {
        TakeDamage(dmg / Armor);
    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg / Armor);
    }
    public void TakeLaserDamage(float dmg)
    {
        TakeDamage(dmg);
    }

    IEnumerator FlashDamage()
    {
        enemyMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        enemyMeshRenderer.material.color = enemyColor;
    }

    IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(stunTime);
        chargeState.isUnstunned = true;
        chargeState.isStunned = false;

    }
    IEnumerator Timer()
    {
        timerOn = true;
        yield return new WaitForSeconds(2);
        timerOn = false;

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

    public void Respawn()
    {
        Destroy(gameObject);
    }


    public void FallIntoTheVoid()
    {
        Destroy(gameObject);
    }
}
