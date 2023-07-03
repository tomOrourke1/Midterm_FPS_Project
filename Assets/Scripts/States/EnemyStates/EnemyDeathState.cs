using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public override void OnEnter()
    {
        base.OnEnter();
        //agent.SetDestination(agent.transform.position);
        agent.isStopped = true;
    }
}
