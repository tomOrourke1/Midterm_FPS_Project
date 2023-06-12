using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveAwayState : EnemyState
{
    [SerializeField] float pointDistance;



    public override void Tick()
    {
        var awayPos = transform.position - GameManager.instance.GetPlayerObj().transform.position;
        awayPos.Normalize();

        awayPos *= pointDistance;
        NavMeshHit hit;
        NavMesh.SamplePosition(awayPos, out hit, 100, 1);
        agent.SetDestination(awayPos + transform.position);
    }


}
