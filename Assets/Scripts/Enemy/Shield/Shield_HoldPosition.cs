using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shield_HoldPosition : EnemyState
{
    // Will try to slowly strafe into position with a shield

    // Enemy Changes: Moves Slower
    // On Enter: Take out shield
    // On Exit:

    [SerializeField] float dist;

    bool strafeRight;

    public override void Tick()
    {

        var playerPos = GameManager.instance.GetPlayerObj().transform.position;
        var dirToPlayer = (playerPos - transform.position);
        dirToPlayer.y = 0;
        dirToPlayer.Normalize();
        var dirToStrafe = Vector3.Cross(dirToPlayer, Vector3.up);

        var leftOrRight = Vector3.Dot(dirToStrafe, dirToPlayer);

        Vector3 pos;

        if (leftOrRight < 0)
        {
            // Right
            pos = (-1 * dirToStrafe * dist) + transform.position;
        } 
        else if (leftOrRight > 0)
        {
            // Left
            pos = (dirToStrafe * dist) + transform.position;
        }
        else
        {
            // Nowhere
            pos = Vector3.zero;
        }


        agent.SetDestination(pos);



        var gameObj = agent.gameObject;

        gameObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);


    }
}
