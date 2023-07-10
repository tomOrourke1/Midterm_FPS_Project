using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState : MonoBehaviour, IState
{
    [Header("--- Componnets ---")]
    [SerializeField] protected NavMeshAgent agent;

    [Header("--- State Agent Values ---")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float acceleration;
    [SerializeField] protected float rotSpeed;
    [SerializeField] protected float stoppingDist;

    [Header("--- Audio ----")]
    [SerializeField] protected EnemyAudio audioScript;

    private void Start()
    {
        audioScript = transform.parent.GetComponentInParent<EnemyAudio>();
    }

    protected void SetAgent()
    {
        agent.speed = moveSpeed;
        agent.acceleration = acceleration;
        agent.angularSpeed = rotSpeed;
        agent.stoppingDistance = stoppingDist;
    }


    public virtual void OnEnter()
    {
        SetAgent();
    }

    public virtual void OnExit()
    {

    }

    public virtual void Tick()
    {

    }

    public virtual bool ExitCondition()
    {
        return false;
    }

}
