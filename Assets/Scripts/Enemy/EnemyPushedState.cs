using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyPushedState : EnemyState
{
    [SerializeField] Rigidbody rb;
    [SerializeField] EnemyAnimaterScript enAmin;
    [SerializeField] EnemyBase enemy;
    public override void OnEnter()
    {
        base.OnEnter();

        rb.isKinematic = false;
        enAmin.StartPush();
    }


    public override void Tick()
    {
        enemy.Landed = enemy.RayGroundCheck() && rb.velocity.y <= 0;
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
