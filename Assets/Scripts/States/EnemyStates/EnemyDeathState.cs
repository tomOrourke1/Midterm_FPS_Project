using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    [Space]
    [SerializeField] EnemyBase enemy;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider colliderd;

    bool die = false;
    bool run = false;
    public override void OnEnter()
    {
        base.OnEnter();

        //var hit = SamplePoint(agent.gameObject.transform.position, 1000, out bool b);
        //if (b)
        //{
        //    agent.ResetPath();
        //    agent.SetDestination(hit.position);
        //}


        SetUpDeath();
        audioScript.PlayEnemy_Death();


        die = true;
        run = true;
    }

    public override void Tick()
    {
        SetUpDeath();




    }
    public override void OnExit()
    {
        run = false;
    }

    private void SetUpDeath()
    {
        die = true;
        
    }

    private void FixedUpdate()
    {
        if (!run)
            return;

        if(die)
        {
            die = false;
            if (enemy.RayGroundCheck())
            {
                if (agent.enabled)
                {
                    agent.SetDestination(agent.gameObject.transform.position);
                }
                //Debug.LogError("I'M IN THE DEATH STATE WHEN THEIS IS HAPPENING");

                //Debug.LogError("vel: " + rb.velocity);


                agent.enabled = false;
                rb.velocity = Vector3.zero;
                colliderd.enabled = false;
                rb.isKinematic = true;

            }


        }
    }

}
