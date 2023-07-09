using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunState : EnemyState
{
    [Space]
    [SerializeField] float stunTime;
    [SerializeField] Rigidbody rb;
    [HideInInspector]
    public bool isStunned;

    bool exit;
    public override void OnEnter()
    {
        base.OnEnter();
        //agent.SetDestination(transform.position);
        isStunned = true;
        exit = false;

        StartCoroutine(DumbTimer());
    }


    public override void Tick()
    {

    }

    public override void OnExit()
    {
        isStunned = false;
        StopAllCoroutines();

        rb.isKinematic = true;
        agent.enabled = true;


    }


    public override bool ExitCondition()
    {
        return exit;
    }


    IEnumerator DumbTimer()
    {
        yield return new WaitForSeconds(stunTime);
        exit = true;
    }

}
