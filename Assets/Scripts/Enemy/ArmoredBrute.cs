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
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float playerFaceSpeed;
    [SerializeField] float timeBetweenCharges;

    [Header("--- movement ranges ----")]
    [SerializeField] float moveDistance;
    [SerializeField] float chargeDistance;
    [SerializeField] float attackDistance;
    [Range(1, 10)][SerializeField] int Armor;

    [SerializeField] NavMeshAgent agent;
    Rigidbody rb;

    [Header("Hit SFX")]
    [SerializeField] EnemyAudio audScript; 

    public UnityEvent OnEnemyDeathEvent;
   




    float timeToNextCharge;
    float timeToNextAttack;
    bool canCharge;
    bool canAttack;



    bool hitWall;


    void Start()
    {
        audScript = GetComponent<EnemyAudio>();

        health.FillToMax();
        enemyColor = enemyMeshRenderer.material.color;

        stateMachine = new StateMachine();
        stateMachine.SetState(idleState);

        stateMachine.AddTransition(idleState, chasePlayerState, OnMove);
        stateMachine.AddTransition(chasePlayerState, idleState, OnIdle);

        stateMachine.AddTransition(chasePlayerState, meleeState, OnAttack);
        stateMachine.AddTransition(idleState, meleeState, OnAttack);
        stateMachine.AddTransition(meleeState, idleState, meleeState.ExitCondition);

        stateMachine.AddTransition(chasePlayerState, chargePrepState, OnChargePrep);
        stateMachine.AddTransition(idleState, chargePrepState, OnChargePrep);


        stateMachine.AddTransition(chargePrepState, chargeState, chargePrepState.ExitCondition);
        stateMachine.AddTransition(chargeState, stunState, OnStun);

        stateMachine.AddTransition(stunState, idleState, OnUnstun);





        // any transitions
        stateMachine.AddAnyTransition(deathState, () => isDead);

        rb = GetComponent<Rigidbody>();

        hasLanded = true;
    }

    void Update()
    {
        if (enemyEnabled)
        {
            stateMachine.Tick();

            if (GetDoesSeePlayer() && !isDead && chargePrepState.isCharging == false && chargeState.isCharging == false)
            {
                RotToPlayer();
            }
        }
    }
    bool OnIdle()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance <= moveDistance && distance > attackDistance;
        return inDistance;
    }

    bool OnMove()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance > moveDistance;

        return inDistance;
    }

    bool OnAttack()
    {
        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance <= attackDistance;


        timeToNextAttack += Time.deltaTime;
        canAttack = timeToNextAttack >= timeBetweenAttacks;
        var temp = canAttack;

        if (temp)
        {
            canAttack = false;
            timeToNextAttack = 0;
        }


        return inDistance && temp;
    }
    bool OnChargePrep()
    {
        // if the arbitrary timer has completed
        // and within charge range

        float distance = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
        bool inDistance = distance <= chargeDistance;


        timeToNextCharge += Time.deltaTime;
        canCharge = timeToNextCharge >= timeBetweenCharges;
        var temp = canCharge;

        if(temp)
        {
            canCharge = false;
            timeToNextCharge = 0;
        }
        
        return inDistance && temp && GetDoesSeePlayer();
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
        //GetComponent<Collider>().enabled = false;
        // GetComponent<NavMeshAgent>().enabled = false;
        //GetComponent<Rigidbody>().isKinematic = true;
        StopAllCoroutines();
        enemyMeshRenderer.material.color = enemyColor;
    }
    bool OnStun()
    {

        bool temp = hitWall;

        if (temp)
            hitWall = false;

        return chargeState.ExitCondition() || temp;
    }
    bool OnUnstun()
    {
        RotToPlayer();
        return stunState.ExitCondition();
    }
    public void TakeDamage(float dmg)
    {

        RotToPlayer();

        health.Decrease(dmg / Armor);
        audScript.PlayEnemy_Hurt();
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
        OnDeath();
        Destroy(gameObject);
    }



    private void OnCollisionEnter(Collision collision)
    {
        foreach (var cont in collision.contacts)
        {
            var norm = cont.normal;
            var dot = Vector3.Dot(norm, Vector3.up);
            if (dot < 0.1f && dot > -0.1f)
            {
                hitWall = true;
                return;
            }
        }
    }

}
