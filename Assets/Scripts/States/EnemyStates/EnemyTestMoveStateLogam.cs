using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyTestMoveStateLogam : EnemyState
{
    public override void Tick()
    {
        // agent?.SetDestination((GameManager.instance.GetPlayerObj().transform.position));


        //var hit = SamplePoint(GameManager.instance.GetPlayerObj().transform.position, 1000, out bool b);
        //if (b)
        //{
        //    agent.ResetPath();
        //    agent.SetDestination(hit.position);
        //}

        if(agent.enabled)
        {
            agent.SetDestination(GameManager.instance.GetPlayerObj().transform.position);
        }

    }
}
