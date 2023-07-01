using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    StateMachine stateMachine;

    [Header("--- componenets ---")]
    [SerializeField] PlayerCooldownBehaviour dashCoolDown1;
    [SerializeField] PlayerCooldownBehaviour dashCoolDown2;
    [SerializeField] PlayerCooldownBehaviour betweenDashCoolDown;

    [Header("--- states --- ")]
    [SerializeField] PlayerMovementState playerMoveState;
    [SerializeField] PlayerDashState dashState;





    private void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.SetState(playerMoveState);


        stateMachine.AddTransition(playerMoveState, dashState, OnDash);
        stateMachine.AddTransition(dashState, playerMoveState, dashState.ExitCondition);




    }


    bool OnDash()
    {
        bool inp = Input.GetKeyDown(KeyCode.LeftShift);

        if (inp && betweenDashCoolDown.CanActivate())
        {
            if(dashCoolDown1.CanActivate())
            {
                return true;
            }
            else if(dashCoolDown2.CanActivate())
            {
                return true;
            }
        }

        return false;
    }





    private void Update()
    {
        stateMachine.Tick();
    }




}
