using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeState : EnemyState
{
   
    [SerializeField] Collider meleeCollider;
   // [SerializeField] EnemyAnimaterScript animScript;

    [SerializeField] float totalTime;
    float timeInState;
    bool exit;
    public override void OnEnter()
    {
        base.OnEnter();
        meleeCollider.enabled = true;
        exit = false;
       // animScript?.StartMelee();
        timeInState = Time.time;
    }


    public override void Tick()
    {
        exit = ((Time.time - timeInState) >= totalTime);
    }

    public override void OnExit()
    {
        meleeCollider.enabled = false;
    //   animScript?.StopMelee();
    }

    public override bool ExitCondition()
    {
        return exit;
    }
    
}
