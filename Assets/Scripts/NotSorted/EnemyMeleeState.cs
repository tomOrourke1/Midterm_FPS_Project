using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeState : EnemyState
{
   
    [SerializeField] Collider meleeCollider;
    [SerializeField] EnemyAnimaterScript animScript;
    [SerializeField] Animator animator;

    [SerializeField] float totalTime;
    float timeInState;
    bool exit;
    public override void OnEnter()
    {
        base.OnEnter();
        meleeCollider.enabled = true;
        exit = false;
        animScript?.StartMelee();
        timeInState = Time.time;
        animator.applyRootMotion = true;
    }


    public override void Tick()
    {
        exit = ((Time.time - timeInState) >= totalTime);
    }

    public override void OnExit()
    {
        meleeCollider.enabled = false;

        if(animScript.DoingMelee)
           animScript?.StopMelee();

        animator.applyRootMotion = false;
        
    }

    public override bool ExitCondition()
    {
        return exit || !animScript.DoingMelee;
    }
    
}
