using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestStateMachine : MonoBehaviour
{
    StateMachine _stateMachine;
    NavMeshAgent agent;


    [SerializeField] Transform goToPosition;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        _stateMachine = new StateMachine();
        var chasePlayer = new ChasePlayer(agent);
        var idle = new Idle();
        var attack = new AttackPlayer();
        var dead = new DeadState();

        _stateMachine.Add(idle);
        _stateMachine.Add(chasePlayer);
        _stateMachine.Add(attack);
        


        _stateMachine.AddTransition(idle, chasePlayer, () => Vector3.Distance(agent.transform.position, goToPosition.position) > 3);
        _stateMachine.AddTransition(chasePlayer, attack, () => Vector3.Distance(agent.transform.position, goToPosition.position) < 3f);
        _stateMachine.AddAnyTransition(dead, () => Vector3.Distance(agent.transform.position, goToPosition.position) > 10f);

        _stateMachine.SetState(idle);
    }



    private void Update()
    {
        _stateMachine.Tick();
    }



}
