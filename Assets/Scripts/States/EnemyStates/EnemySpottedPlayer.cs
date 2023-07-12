using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpottedPlayer : EnemyState
{
    private Vector3 spotPosition;

    public override void OnEnter()
    {
        base.OnEnter();

        spotPosition = GameManager.instance.GetPlayerPOS();
    }

    public override void Tick()
    {

        //var hit = SamplePoint(spotPosition, 1000, out bool b);
        //if (b)
        //{

        //    agent.ResetPath();
        //    agent.SetDestination(hit.position);
        //}

        if(agent.enabled)
        {
            agent.SetDestination(spotPosition);
        }


      //  agent?.SetDestination(spotPosition);
    }
}
