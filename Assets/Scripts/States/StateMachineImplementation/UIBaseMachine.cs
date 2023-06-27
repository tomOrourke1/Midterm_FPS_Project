using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseMachine : MonoBehaviour
{
    // the state machine // DONT TOUCH
    protected StateMachine statemachine;


    [Header("--- States ---")]
    [SerializeField] UIMenuState playState;

    // example of where to add states
    [SerializeField] UIMenuState radialState;
    [SerializeField] UIMenuState pauseMenuState;
    [SerializeField] UIMenuState settingsMenuState;


    // async bools
    AsyncInput pauseBool;


    private void Start()
    {
        // initialize objectes
        statemachine = new StateMachine();
        statemachine.SetState(playState);

        // set up the inputs
        pauseBool = new AsyncInput();


        // this is where you set up state transitions
        statemachine.AddTransition(playState, pauseMenuState, () => pauseBool.GetInput());




        
    }


    // set functions for bool values
    public void SetPause(bool value)
    {
        pauseBool.SetInput(value);
        Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }









    // shouldn't need to touch
    private void Tick()
    {
        statemachine.Tick();
    }
}
