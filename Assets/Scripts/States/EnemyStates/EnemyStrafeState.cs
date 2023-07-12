using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrafeState : EnemyState
{
    [SerializeField] float dist;

    bool strafeRight; 




    public override void Tick()
    {

        var playerPos = GameManager.instance.GetPlayerObj().transform.position;
        var dirToPlayer = (playerPos - transform.position);
        dirToPlayer.y = 0;
        dirToPlayer.Normalize();
        var dirToStrafe = Vector3.Cross(dirToPlayer, Vector3.up);



        var pos = (dirToStrafe * dist) + transform.position;

        //var hit = SamplePoint(pos, 1000, out bool b);
        //if (b)
        //{

        //    agent.ResetPath();
        //    agent.SetDestination(hit.position);
        //}


        if(agent.enabled)
        {
            agent.SetDestination(pos);
        }

      //  agent?.SetDestination(pos);



        var gameObj = agent.gameObject;
        
        gameObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);


    }

}
