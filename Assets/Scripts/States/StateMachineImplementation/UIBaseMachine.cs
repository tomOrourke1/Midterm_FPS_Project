using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseMachine : MonoBehaviour
{
    // the state machine // DONT TOUCH
    protected StateMachine statemachine;


    [Header("--- States ---")]
    [SerializeField] UIMenuState playState;
    [SerializeField] UIWaitingScript waitForPauseToStop;
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
        deathBool = new AsyncInput();

        // this is where you set up state transitions
        
        // Play <- (Pause Transition out) -> Pause
        statemachine.AddTransition(playState, pausedState, () => onEscape.GetInput());
        statemachine.AddTransition(pausedState, waitForPauseToStop, () => onEscape.GetInput());
        statemachine.AddTransition(waitForPauseToStop, playState, () => waitForPauseToStop.ExitCondition());

        // Play <-> Death
        statemachine.AddTransition(playState, deathState, () => deathBool.GetInput());
        statemachine.AddTransition(deathState, playState, () => playBool.GetInput());

        // Play <-> Radial Menu

        // Play <-> Infographic

        // Paused <-> Main Menu

        // Paused <-> Settings

        // Settings <-> Options

        // Settings <-> Controls

        // Options <-> Controls
    }


    // set functions for bool values
    public void SetPause(bool value)
    {
        pauseBool.SetInput(value);
        //Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }

    // set functions for bool values
    public void SetPlay(bool value)
    {
        playBool.SetInput(value);
        //Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }

    // set functions for bool values
    public void SetOnEscape(bool value)
    {
        onEscape.SetInput(value);
        //Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }

    // set functions for bool values
    public void SetDeath(bool value)
    {
        deathBool.SetInput(value);
        //Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }



    private void Update()
    {
        Tick();
    }

    // shouldn't need to touch
    private void Tick()
    {
        statemachine.Tick();
    }
}
