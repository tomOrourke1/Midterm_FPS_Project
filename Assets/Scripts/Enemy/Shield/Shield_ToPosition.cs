using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_ToPosition : EnemyState
{
    // Will quickly move into position without a shield

    // Enemy Changes: Moves at normal speed
    // On Enter: Put away shield
    // On Exit: 



    bool exit;

    public override void Tick()
    {

        //var dist = Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, transform.position);


        agent.SetDestination(GameManager.instance.GetPlayerPOS());
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
