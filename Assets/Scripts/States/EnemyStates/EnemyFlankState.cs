using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlankState : EnemyState
{
    [Header("vals")]
    [SerializeField] float dist;

    public override void Tick()
    {

        agent.SetDestination(GetBehindPlayer());


    }


    public Vector3 GetBehindPlayer()
    {

        var dir = -Camera.main.transform.forward;
        dir.y = 0;
        dir.Normalize();


        return (dist * dir) + GameManager.instance.GetPlayerPOS();

    }



}
