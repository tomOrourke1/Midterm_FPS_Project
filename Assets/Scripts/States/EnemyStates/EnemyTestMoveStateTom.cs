using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestMoveStateTom : EnemyState
{



    public override void Tick()
    {


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

        //agent?.SetDestination(GameManager.instance.GetPlayerObj().transform.position);

    }




}
