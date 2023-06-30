using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimaterScript : MonoBehaviour
{

    [SerializeField] Animator enemyAnimator;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform enemyBaseTransform;




    private void Update()
    {
        TickMovement();
    }

    public void TickMovement()
    {

        var y = Vector3.Dot(enemyBaseTransform.forward, agent.velocity);
        var x = Vector3.Dot(enemyBaseTransform.right, agent.velocity);



        enemyAnimator.SetFloat("X", x);
        enemyAnimator.SetFloat("Y", y);

    }


    public void PlayShoot()
    {
        enemyAnimator.SetBool("Shoot", true);
    }

    public void StopShoot()
    {
        enemyAnimator.SetBool("Shoot", false);

    }





}