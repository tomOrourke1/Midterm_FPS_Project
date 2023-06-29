using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class UIBaseMachine : MonoBehaviour
{
    // the state machine // DONT TOUCH
    protected StateMachine statemachine;

    // States and Async's in a region to hide lines
    #region States and Asyncs
    [Header("--- States ---")]
    [SerializeField] UIMenuState playState;
    [SerializeField] UIWaitingScript waitForPauseToStop;
    [SerializeField] UIMenuState pausedState;
    [SerializeField] UIMenuState radialState;
    [SerializeField] UIMenuState settingState;
    [SerializeField] UIMenuState deathState;
    [SerializeField] UIMenuState infographicState;


    // async bools
    AsyncInput playBool;
    AsyncInput pauseBool;
    AsyncInput radialBool;
    AsyncInput settingBool;
    AsyncInput deathBool;
    AsyncInput infographicBool;
    AsyncInput onEscape;
    AsyncInput deathContinueBool;
    AsyncInput interactBool;
    #endregion

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
        infographicBool = new AsyncInput();
        settingBool = new AsyncInput();
        radialBool = new AsyncInput();
        deathContinueBool = new AsyncInput();
        interactBool = new AsyncInput();

        #region State Transitions
        // Play <- (Pause Transition out) -> Pause
        statemachine.AddTransition(playState, pausedState, () => onEscape.GetInput());
        statemachine.AddTransition(pausedState, waitForPauseToStop, () => onEscape.GetInput());
        statemachine.AddTransition(waitForPauseToStop, playState, () => waitForPauseToStop.ExitCondition());

        // Play <-> Death
        statemachine.AddTransition(playState, deathState, () => deathBool.GetInput());
        statemachine.AddTransition(deathState, playState, () => deathContinueBool.GetInput());

        // Play <-> Radial Menu
        statemachine.AddTransition(playState, radialState, () => radialBool.GetInput());
        statemachine.AddTransition(radialState, playState, () => playBool.GetInput());

        // Play <-> Infographic
        statemachine.AddTransition(playState, infographicState, () => infographicBool.GetInput());
        statemachine.AddTransition(infographicState, playState, () => interactBool.GetInput());

        // Paused <-> Main Menu
        // Not Done yet...

        // Paused <-> Settings
        statemachine.AddTransition(pausedState, settingState, () => settingBool.GetInput());
        statemachine.AddTransition(settingState, pausedState, () => onEscape.GetInput());
        #endregion
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

    // set functions for bool values
    public void SetInfo(bool value)
    {
        infographicBool.SetInput(value);
        //Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }
    
    // set functions for bool values
    public void SetSettingsAsync(bool value)
    {
        settingBool.SetInput(value);
        //Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }
    
    // set functions for bool values
    public void SetRadialAsync(bool value)
    {
        radialBool.SetInput(value);
        //Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }

    // set functions for bool values
    public void SetDeathContinueBool(bool value)
    {
        deathContinueBool.SetInput(value);
        //Tick(); //                  <<<< require a tick here so when a value is updated you it check the transitions
    }

    // set functions for bool values
    public void SetInteract(bool value)
    {
        interactBool.SetInput(value);
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
