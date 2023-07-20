using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyPushedState : EnemyState
{
    [SerializeField] Rigidbody rb;
    [SerializeField] EnemyAnimaterScript enAmin;
    [SerializeField] EnemyBase enemy;

    int frames;
    public override void OnEnter()
    {
        base.OnEnter();

        rb.isKinematic = false;
        enAmin.StartPush();
        frames = 0;

        enemy.Landed = false;
    }


    public override void Tick()
    {
        if (frames > 1)
        {
            enemy.Landed = enemy.RayGroundCheck() && rb.velocity.y <= 0 && rb.velocity.magnitude < 0.1f;
            //Debug.LogError("Vel: " + rb.velocity);
           //Debug.LogError("enemy is in tick here");

            if (enemy.Landed && rb.velocity.y < 0)
            {
             //   Debug.LogError("ENEMY GOING DOWN");
                var v = rb.velocity;
                v.y = 0;
                rb.velocity = v;
            }
        }
        frames++;
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
