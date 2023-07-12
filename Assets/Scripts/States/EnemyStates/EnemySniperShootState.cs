using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySniperShootState : EnemyState
{
    [Header("--- gun values ---")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gunPos;
    bool exit;
    bool fired;
    float timeInState;


    [Header("--- shoot values ---")]
    [SerializeField] float timeToShoot;
    [SerializeField] float totalTime;

    [Header("line stuff")]
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float lineThickness;
    float currentThickness;
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

        //var hit = SamplePoint(agent.gameObject.transform.position, 1000, out bool b);
        //if (b)
        //{

        //    agent.ResetPath();
        //    agent.SetDestination(hit.position);
        //}

        if(agent.enabled)
        {
            agent.SetDestination(agent.gameObject.transform.position);
        }


        playerPos = GameManager.instance.GetPlayerPOS();
        lineRenderer.enabled = true;
    }

    public override void Tick()
    {

        LineThickness();
        DrawLine();


        if (!fired && (Time.time - timeInState) >= timeToShoot)
        {
            fired = true;
            var dir = GameManager.instance.GetPlayerPOS() - gunPos.position;
            Instantiate(bullet, gunPos.position, Quaternion.LookRotation(dir));

            lineRenderer.enabled = false;
        }

        exit = ((Time.time - timeInState) >= totalTime);
    }


    void DrawLine()
    {
        RaycastHit hit;
        bool doesHit = Physics.Raycast(gunPos.position, (GameManager.instance.GetPlayerPOS() - gunPos.position).normalized, out hit);

        if(doesHit)
        {

            lineRenderer.SetPosition(0, gunPos.position);
            lineRenderer.SetPosition(1, hit.point);
        }


    }


    void LineThickness()
    {
        var ratio = (Time.time - timeInState) / timeToShoot;

        var thick = lineThickness * ratio;


        lineRenderer.startWidth = thick;
        lineRenderer.endWidth = thick;
    }



    public override bool ExitCondition()
    {
        return exit;
    }

    public override void OnExit()
    {
        lineRenderer.enabled = false;
        timeInState = Time.time;
    }
}
