using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlankState : EnemyState
{
    [Header("vals")]
    [SerializeField] float dist;

    public override void Tick()
    {
        //var hit = SamplePoint(GetBehindPlayer(), 1000, out bool b);
        //if (b)
        //{
        //    agent.ResetPath();
        //    agent.SetDestination(hit.position);
        //}

        if(agent.enabled)
        {
            agent.SetDestination(GetBehindPlayer());
        }


        //agent?.SetDestination(GetBehindPlayer());


    }


    public Vector3 GetBehindPlayer()
    {

        var dir = -Camera.main.transform.forward;
        dir.y = 0;
        dir.Normalize();


        return (dist * dir) + GameManager.instance.GetPlayerPOS();

    }



}
