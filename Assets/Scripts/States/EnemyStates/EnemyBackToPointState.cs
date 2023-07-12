using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackToPointState : EnemyState
{


    Vector3 point;


    public override void OnEnter()
    {
        base.OnEnter();

    }


    public override void Tick()
    {

        //var hit = SamplePoint(point, 1000, out bool b);
        //if (b)
        //{

        //    agent.ResetPath();
        //    agent.SetDestination(hit.position);
        //}

        if(agent.enabled)
        {

            agent.SetDestination(point);
        }

        //agent?.SetDestination(point);

    }


    public void SetDestination(Vector3 point)
    {
        this.point = point;
    }

}
