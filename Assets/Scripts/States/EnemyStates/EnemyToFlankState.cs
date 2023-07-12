using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyToFlankState : EnemyState
{

    [Header("vals")]
    [SerializeField] float dist;
    [SerializeField] float allowedDot;
    bool exit;



    public override void OnEnter()
    {
        base.OnEnter();

        exit = false;

    }

    public override void Tick()
    {
        var playerPos = GameManager.instance.GetPlayerPOS();
        var playerDir = Camera.main.transform.forward;
        var playerRight = Camera.main.transform.right;
        playerDir.y = 0;
        playerDir.Normalize();
        var dirToEnemy = transform.position - playerPos;

        var dot = Vector3.Dot(dirToEnemy, playerRight);

        var sideMod = dot > 0 ? -1 : 1;

        if(Vector3.Dot(playerDir, dirToEnemy) > 0)
        {
            dirToEnemy = Vector3.ProjectOnPlane(dirToEnemy, Vector3.up);

            var projectedPoint = Vector3.Project(dirToEnemy, playerDir);
            var pointInWorld = (projectedPoint * 0.5f) + playerPos;


            var dirToPointInWorld = (pointInWorld - transform.position).normalized;

            var cross = Vector3.Cross(Vector3.up, (dirToPointInWorld));

            var newPos = (cross * dist * sideMod) + playerPos;



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
        else
        {
            exit = true;
        }


    }


    public override bool ExitCondition()
    {
        return exit;
    }





}
