using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeState : EnemyState
{
    public Vector3 forceDirection;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider attackTrigger;
    [SerializeField] Collider detectTrigger;
    [SerializeField] EnemyChargePrepState chargePrepState;
    [SerializeField] int chargeDamage;
    [SerializeField] float force;
    [SerializeField] float upwardForce;

    [SerializeField] public float chargeSpeed;

    [SerializeField] public bool isUnstunned;
    [SerializeField] public bool isStunned;
    public bool hitEntity;
    bool exit;
    public override void OnEnter()
    {
        base.OnEnter();
        forceDirection = gameObject.transform.forward;
        rb.freezeRotation = true;
        LaunchCharge();
       
    }


    public override void Tick()
    {
     
    }

    public override void OnExit()
    {
      attackTrigger.enabled = false;
        detectTrigger.enabled = true;
    }


    public override bool ExitCondition()
    {
        return exit;
    }
    void LaunchCharge()
    {

        agent.enabled = false;
        rb.isKinematic = false;
       
        Vector3 speed = forceDirection * chargeSpeed;
        rb.AddForce(speed, ForceMode.Force);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
            {
                IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
                damagable.TakeDamage(chargeDamage);
                var applyVel = other.gameObject.GetComponent<IApplyVelocity>();
             if (applyVel != null)
             {
                forceDirection = gameObject.transform.forward;
                Vector3 velocity = forceDirection * force + transform.up * upwardForce;
                applyVel.ApplyVelocity(velocity);
                hitEntity = true;
             }

            }
            else
            {
                isStunned = true;
                isUnstunned = false;
                chargePrepState.isCharging = false;
            }
          
            exit = true;
    }
    private void OnCollisionEnter(Collision collision)
    {

        foreach (var cont in collision.contacts)
        {
            var norm = cont.normal;
            if (Vector3.Dot(norm, Vector3.up) > 0.8f)
            {
                
                return;
            }
        }

    }
}
