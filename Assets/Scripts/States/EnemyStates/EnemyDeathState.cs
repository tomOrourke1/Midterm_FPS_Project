using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    [Space]
    [SerializeField] EnemyBase enemy;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider colliderd;
    

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
       


    }

    public override void Tick()
    {
        SetUpDeath();




    }


    private void SetUpDeath()
    {
        if (enemy.RayGroundCheck())
        {
            if (agent.enabled)
            {
                agent.SetDestination(agent.gameObject.transform.position);
            }

            agent.enabled = false;
            rb.velocity = Vector3.zero;
            audioScript.PlayEnemy_Death();
            colliderd.enabled = false;
            rb.isKinematic = true;

        }
    }

}
