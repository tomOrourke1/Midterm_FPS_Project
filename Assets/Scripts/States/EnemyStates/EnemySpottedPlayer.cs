using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpottedPlayer : EnemyState
{
    private Vector3 spotPosition;

    public override void OnEnter()
    {
        base.OnEnter();

        spotPosition = GameManager.instance.GetPlayerPOS();
    }

    public override void Tick()
    {
        agent.SetDestination(spotPosition);
    }
}
