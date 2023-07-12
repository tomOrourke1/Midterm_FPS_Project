using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{

    public override void Tick()
    {
    }


    public override void OnExit()
    {

    }


    public override void OnEnter()
    {
        base.OnEnter(); // this has to be here

        //var hit = SamplePoint(transform.position, 1000, out bool b);
        //if (b)
        //{
        //    agent.ResetPath();
        //    agent.SetDestination(hit.position);
        //}

        if (agent.enabled)
        {
            agent.SetDestination(transform.position);
        }


    }


}
