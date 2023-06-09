using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimaterScript : MonoBehaviour
{

    [SerializeField] Animator enemyAnimator;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform enemyBaseTransform;


    bool meleeing;

    public bool DoingMelee => meleeing;

    private void Update()
    {
        TickMovement();
    }

    public void TickMovement()
    {

        var y = Vector3.Dot(enemyBaseTransform.forward, agent.velocity);
        var x = Vector3.Dot(enemyBaseTransform.right, agent.velocity);

        y = y / agent.speed;
        y = x / agent.speed;

       // y = Mathf.Clamp(y, -1, 1);
       // y = Mathf.Round(y);

       // x = Mathf.Clamp(x, -1, 1);
       // x = Mathf.Round(x);

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

    public void SetDeath()
    {
        enemyAnimator.SetBool("Death", true);
        enemyAnimator.SetTrigger("OnDeath");
        enemyAnimator.applyRootMotion = true;
    }

    public void StartMelee()
    {
        enemyAnimator.SetTrigger("Meloo");
        enemyAnimator.SetBool("Melee", true);
        meleeing = true;
    }
    
    public void StopMelee()
    {
        enemyAnimator.SetBool("Melee", false);
        meleeing = false;

    }

    public void TransposeBody()
    {
        var pos = transform.localPosition;
        pos.y -= 1;
        transform.localPosition = pos;
    }


}
