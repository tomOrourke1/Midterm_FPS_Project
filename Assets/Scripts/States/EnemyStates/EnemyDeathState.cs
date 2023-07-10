using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    [Space]
    [SerializeField] Rigidbody rb;

    public override void OnEnter()
    {
        base.OnEnter();
        agent.enabled = false;
        rb.velocity = Vector3.zero;
        audioScript.PlayEnemy_Death();
    }
}
