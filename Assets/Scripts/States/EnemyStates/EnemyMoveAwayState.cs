using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveAwayState : EnemyState
{
    [Header("Values")]
    [SerializeField] float pointDistance;



    bool canExit;

    public override void Tick()
    {

        var direction = transform.position - GameManager.instance.GetPlayerObj().transform.position;
        direction.Normalize();

        direction *= pointDistance;

        var newPos = direction + transform.position;

        //var hit = SamplePoint(newPos, 1000, out bool b);
        //if (b)
        //{
        //    agent.ResetPath();
        //    agent.SetDestination(hit.position);
        //}

        if(agent.enabled)
        {
            agent.SetDestination(newPos);
        }

       // agent?.SetDestination(newPos);


    }


    public override void OnEnter()
    {
        base.OnEnter();
        canExit = false;


    }

    public override void OnExit()
    {



    }

    public override bool ExitCondition()
    {
        return canExit;
    }
}
