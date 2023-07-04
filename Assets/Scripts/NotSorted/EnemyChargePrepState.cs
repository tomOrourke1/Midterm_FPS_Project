using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargePrepState : EnemyState
{
    public bool isCharging;
    [SerializeField] float chargeTime;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider attackTrigger;
    [SerializeField] Collider detectTrigger;
    public override void OnEnter()
    {
        base.OnEnter();
        agent.enabled = false;
        rb.isKinematic = true;
        attackTrigger.enabled = true;
        detectTrigger.enabled = false;
        StartCoroutine(ChargeTimer());
    }


    public override void Tick()
    {

    }

    public override void OnExit()
    {

    }


    public override bool ExitCondition()
    {
        return isCharging;
    }
    IEnumerator ChargeTimer()
    {
        yield return new WaitForSeconds(chargeTime);
        isCharging = true;
        
    }
}
