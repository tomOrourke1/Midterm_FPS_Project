using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public override void Tick()
    {
        agent.SetDestination((GameManager.instance.GetPlayerObj().transform.position));
        Debug.Log(agent.acceleration + "ACCEL");
        Debug.Log(agent.remainingDistance + "REMAINING");
    }
}
