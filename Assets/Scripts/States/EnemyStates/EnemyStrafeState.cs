using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrafeState : EnemyState
{
    [SerializeField] float dist;

    bool strafeRight; 

    public override void Tick()
    {

        var playerPos = GameManager.instance.GetPlayerObj().transform.position;
        var dirToPlayer = (playerPos - transform.position).normalized;
        var dirToStrafe = Vector3.Cross(dirToPlayer, Vector3.up);



        var pos = (dirToStrafe * dist) + transform.position;
        agent.SetDestination(pos);

    }

}
