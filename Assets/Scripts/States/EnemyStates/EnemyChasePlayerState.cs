using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasePlayerState : EnemyState
{


    bool exit;

    public override void Tick()
    {

        //var dist = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position);


        agent.SetDestination(gameManager.instance.GetPlayerObj().transform.position);
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
