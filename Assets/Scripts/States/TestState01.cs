using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestState01 : IState
{
    NavMeshAgent agent;
    public TestState01(NavMeshAgent agent)
    {
        this.agent = agent;
    }
    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        agent.SetDestination(Vector3.zero);
    }
}

public class Idle : IState
{
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}
public class ChasePlayer : IState
{
    NavMeshAgent agent;

    public ChasePlayer(NavMeshAgent agent)
    {
        this.agent = agent;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {

    }
}

public class AttackPlayer : IState
{
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}

public class DeadState : IState
{
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}