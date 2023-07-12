using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasePlayerState : EnemyState
{


    bool exit;

    public override void Tick()
    {

        //var dist = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position);
        var hit = SamplePoint(GameManager.instance.GetPlayerObj().transform.position, 1000, out bool b);
        if (b)
        {
            agent.ResetPath();
            agent.SetDestination(hit.position);
        }


        if(agent.enabled)
        {
            agent.SetDestination(GameManager.instance.GetPlayerObj().transform.position);
        }

       // agent?.SetDestination(GameManager.instance.GetPlayerObj().transform.position);
    }



    public override void OnEnter()
    {
        base.OnEnter();
        exit = false;


    }

    public override void OnExit()
    {
        



    }



    public override bool ExitCondition()
    {
        return exit;
    }

}
