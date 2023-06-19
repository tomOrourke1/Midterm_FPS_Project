using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestMoveStateLogam : EnemyState
{
    public override void Tick()
    {
        agent.SetDestination((GameManager.instance.GetPlayerObj().transform.position));
    }
}
