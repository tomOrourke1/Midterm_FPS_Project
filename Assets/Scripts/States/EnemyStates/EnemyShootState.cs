using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootState : EnemyState
{

    [Header("--- state values ---")]

    [SerializeField] GameObject bullet;

    [SerializeField] Transform gunPos;
    bool exit;
    bool fired;

    [SerializeField] float timeToShoot;
    [SerializeField] float totalTime;
    [SerializeField] float rotationSpeed;

    private void OnValidate()
    {
        timeToShoot = Mathf.Clamp(timeToShoot, 0, totalTime);
    }

    float timeInState;


    public override void OnEnter()
    {
        base.OnEnter();
        exit = false;
        fired = false;
        timeInState = Time.time;

        agent.SetDestination(agent.transform.position);
    }

    public override void Tick()
    {
    //    var g = agent.gameObject;
    //    var dirToPlayer = GameManager.instance.GetPlayerObj().transform.position - gunPos.position;
    //    dirToPlayer.Normalize();
    //    g.transform.rotation = Quaternion.Slerp(g.transform.rotation, Quaternion.LookRotation(dirToPlayer), Time.deltaTime * rotSpeed);


        if (!fired && (Time.time - timeInState) >= timeToShoot)
        {
            fired = true;
            Instantiate(bullet, gunPos.position, gunPos.rotation);
        }

        exit = (Time.time - timeInState) >= totalTime;
    }

    public override bool ExitCondition()
    {
        return exit;
    }

}
