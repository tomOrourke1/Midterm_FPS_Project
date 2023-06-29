using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootState : EnemyState
{

    [Header("Required Components")]
    [SerializeField] EnemyAnimaterScript animScript;

    [Header("--- state values ---")]

    [SerializeField] GameObject bullet;

    [SerializeField] Transform gunPos;
    bool exit;
    bool fired;
    float timeInState;


    [SerializeField] float timeToShoot;
    [SerializeField] float totalTime;
    [SerializeField] float rotationSpeed;

    Vector3 playerPos;

    private void OnValidate()
    {
        timeToShoot = Mathf.Clamp(timeToShoot, 0, totalTime);
    }



    public override void OnEnter()
    {
        base.OnEnter();
        exit = false;
        fired = false;
        timeInState = Time.time;

        agent.SetDestination(agent.transform.position);

        playerPos = GameManager.instance.GetPlayerPOS();

    }

    public override void Tick()
    {
        FacePlayer();

        if (!fired && (Time.time - timeInState) >= timeToShoot)
        {
            fired = true;
            var dir = playerPos - gunPos.position;
            Instantiate(bullet, gunPos.position, Quaternion.LookRotation(dir));

            animScript?.PlayShoot();
        }

        exit = ((Time.time - timeInState) >= totalTime);
    }

    void FacePlayer()
    {
        var g = agent.gameObject;
        var dirToPlayer = GameManager.instance.GetPlayerObj().transform.position - transform.position;
        dirToPlayer.y = 0;
        dirToPlayer.Normalize();
        g.transform.rotation = Quaternion.Slerp(g.transform.rotation, Quaternion.LookRotation(dirToPlayer, Vector3.up), Time.deltaTime * rotSpeed);

    }


    public override void OnExit()
    {

        animScript?.StopShoot();
    }

    public override bool ExitCondition()
    {
        return exit;
    }

}
