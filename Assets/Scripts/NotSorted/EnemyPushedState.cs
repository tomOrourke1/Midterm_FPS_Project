using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyPushedState : EnemyState
{
    [SerializeField] Rigidbody rb;

    public override void OnEnter()
    {
        base.OnEnter();

        rb.isKinematic = false;
    }


    public override void Tick()
    {

    }

    public override void OnExit()
    {

    }


    public override bool ExitCondition()
    {
        return false;
    }
}
