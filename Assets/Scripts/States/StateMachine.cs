using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class StateMachine
{
    
    private Dictionary<IState, List<StateTransition>> _stateTransitions = new Dictionary<IState, List<StateTransition>>();
    private List<StateTransition> _anyStateTransitions = new List<StateTransition>();

    List<IState> states = new List<IState>();
    IState currentState;


    public void Add(IState state)
    {
        states.Add(state);
    }

    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        if(_stateTransitions.ContainsKey(from) == false)
            _stateTransitions[from] = new List<StateTransition>();

        var transition = new StateTransition(from, to, condition);
        _stateTransitions[from].Add(transition);

    }

    public void AddAnyTransition(IState to, Func<bool> condition)
    {
        var transition = new StateTransition(null, to, condition);
        _anyStateTransitions.Add(transition);
    }

    public void SetState(IState state)
    {
        if(currentState == state) 
            return;

        currentState?.OnExit();
        currentState = state;
        currentState?.OnEnter();
    }

    public void Tick()
    {
        var transition = CheckForTransition();
        if(transition != null)
        {
            SetState(transition.To);
        }
        currentState.Tick();
    }

    private StateTransition CheckForTransition()
    {
        foreach(var transition in _anyStateTransitions)
        {
            if(transition.Condition())
                return transition;
        }

        if (_stateTransitions.ContainsKey(currentState))
        {
            foreach(var transition in _stateTransitions[currentState])
            {
                if (transition.Condition())
                    return transition;
            }
        }
        return null;
    }
}


