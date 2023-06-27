using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseMachine : MonoBehaviour
{
    // the state machine // DONT TOUCH
    protected StateMachine statemachine;


    [Header("--- States ---")]
    [SerializeField] UIMenuState playState;
    [SerializeField] UIMenuState pausedState;
    [SerializeField] UIMenuState radialState;
    [SerializeField] UIMenuState settingState;
    [SerializeField] UIMenuState controlsState;
    [SerializeField] UIMenuState optionsState;
    [SerializeField] UIMenuState winState;
    [SerializeField] UIMenuState deathState;
    [SerializeField] UIMenuState cheatState;
    [SerializeField] UIMenuState infographicState;


    // async bools
    AsyncInput playBool;
    AsyncInput pauseBool;
    AsyncInput radialBool;
    AsyncInput settingBool;
    AsyncInput controlBool;
    AsyncInput optionsBool;
    AsyncInput winBool;
    AsyncInput deathBool;
    AsyncInput cheatBool;
    AsyncInput infographicBool;
    AsyncInput onEscape;


    private void Start()
    {
        // initialize objectes
        statemachine = new StateMachine();
        statemachine.SetState(playState);

        // set up the inputs
        pauseBool = new AsyncInput();
        playBool = new AsyncInput();
        onEscape = new AsyncInput();

        // this is where you set up state transitions
        statemachine.AddTransition(playState, pausedState, () => onEscape.GetInput());
        statemachine.AddTransition(pausedState, playState, () => onEscape.GetInput());



        
    }


    // set functions for bool values
    public void SetPause(bool value)
    {
        pauseBool.SetInput(value);
        Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }

    // set functions for bool values
    public void SetPlay(bool value)
    {
        playBool.SetInput(value);
        Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }

    // set functions for bool values
    public void SetOnEscape(bool value)
    {
        onEscape.SetInput(value);
        Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }






    // shouldn't need to touch
    private void Tick()
    {
        statemachine.Tick();
    }
}
