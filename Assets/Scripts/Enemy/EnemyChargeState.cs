using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeState : EnemyState
{
    [Space]
    [SerializeField] Rigidbody rb;
    [SerializeField] int chargeDamage;
    [SerializeField] float force;
    [SerializeField] float upwardForce;

    [SerializeField] public float chargeSpeed;
    [SerializeField] float maxChargeDistance;

    [Header("Colliders")]
    [SerializeField] SphereCollider hitTrigger;
    [SerializeField] SphereCollider hitCollider;


    [HideInInspector]
    public bool isCharging;
    bool exit;
    Vector3 forceDirection;

    Vector3 startPos;

    public bool OverDist
    {
        get
        {
            return isCharging && Vector3.Distance(transform.position, startPos) > maxChargeDistance;
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        isCharging = true;
        forceDirection = gameObject.transform.forward;

        agent.enabled = false;
        rb.isKinematic = false;
        startPos = transform.position;

        hitTrigger.enabled = true;
        //hitCollider.enabled = true;
        exit = false;
    }


    public override void Tick()
    {

        Vector3 speed = forceDirection * chargeSpeed;
        rb.velocity = speed;

    }

    public override void OnExit()
    {
        isCharging = false;
        rb.velocity = Vector3.zero;
        


        hitTrigger.enabled = false;
        hitCollider.enabled = false;
    }


    public override bool ExitCondition()
    {
        return exit || OverDist;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject == agent.gameObject)
                return;
            IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
            damagable?.TakeDamage(chargeDamage);
            var applyVel = other.gameObject.GetComponent<IApplyVelocity>();
            if (applyVel != null)
            {
                forceDirection = gameObject.transform.forward;
                Vector3 velocity = forceDirection * force + transform.up * upwardForce;
                applyVel.ApplyVelocity(velocity);
            }

            exit = true;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {

        foreach (var cont in collision.contacts)
        {
            var norm = cont.normal;
            var dot = Vector3.Dot(norm, Vector3.up);
            if ( dot < 0.1f && dot > -0.1f)
            {
                exit = true;
                return;
            }
        }

    }
}
