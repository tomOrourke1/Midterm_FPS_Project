using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    StateMachine stateMachine;



    [Header("--- states --- ")]
    [SerializeField] PlayerMovementState playerMoveState;
    [SerializeField] PlayerDashState dashState;





    private void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.SetState(playerMoveState);


        




    }




    private void Update()
    {
        stateMachine.Tick();
    }




}
