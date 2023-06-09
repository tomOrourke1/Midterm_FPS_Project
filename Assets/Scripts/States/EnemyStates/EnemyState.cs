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
        audioScript = transform.GetComponentInParent<EnemyAudio>();
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



    public NavMeshHit SamplePoint(Vector3 point, float dist, out bool does)
    {
        NavMeshHit hit;

        does = NavMesh.SamplePosition(point, out hit, dist, 1);
        return hit;
    }

}
