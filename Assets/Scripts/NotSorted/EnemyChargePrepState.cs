using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargePrepState : EnemyState
{
    [Space]
    [SerializeField] float chargeTime;



    [HideInInspector]
    public bool isCharging;

    bool canCharge;
    public override void OnEnter()
    {
        canCharge = false;
        isCharging = true;
        base.OnEnter();
        StartCoroutine(ChargeTimer());
    }


    public override void Tick()
    {

    }

    public override void OnExit()
    {
        isCharging = false;
        StopAllCoroutines();
        
    }


    public override bool ExitCondition()
    {
        return canCharge;
    }
    IEnumerator ChargeTimer()
    {
        yield return new WaitForSeconds(chargeTime);
        canCharge = true;
        
    }
}
