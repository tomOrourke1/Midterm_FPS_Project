using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeState : EnemyState
{
    [Space]
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
        
        animator.gameObject.transform.localPosition = Vector3.zero;
        animator.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 45, 0));
    }

    public override bool ExitCondition()
    {
        Debug.LogError("exit: " + exit + " animdoing: " + animScript.DoingMelee);
        return exit || !animScript.DoingMelee;
    }
    
}
