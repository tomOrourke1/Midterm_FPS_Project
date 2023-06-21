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
        agent.SetDestination(point);
    }


    public void SetDestination(Vector3 point)
    {
        this.point = point;
    }

}
