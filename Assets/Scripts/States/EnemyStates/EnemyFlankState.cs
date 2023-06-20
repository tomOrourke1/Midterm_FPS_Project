using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlankState : EnemyState
{
    [SerializeField] float dist;

    public override void Tick()
    {
        var dir = Camera.main.transform.forward;
        dir.y = 0;
        dir.Normalize();

        var pos = GameManager.instance.GetPlayerPOS() + (-dir * dist);


        agent.SetDestination(pos);


    }
}
