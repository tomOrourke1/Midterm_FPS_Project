using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyPushedState : EnemyState
{
    [SerializeField] Rigidbody rb;
    [SerializeField] EnemyAnimaterScript enAmin;
    public override void OnEnter()
    {
        base.OnEnter();

        rb.isKinematic = false;
        enAmin.StartPush();
    }


    public override void Tick()
    {

    }

    public override void OnExit()
    {
        enAmin.StopPush();
    }


    public override bool ExitCondition()
    {
        return false;
    }
}
