using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamagable
{
    // Enemy Components
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform enemyHeadPos;
    [SerializeField] Transform enemyShootPos;

    // Enemy Stats
    [SerializeField] int enemyHP;
    [SerializeField] int enemyWalkSpeed;
    [SerializeField] int facePlayerSpeed;
    [SerializeField] int viewPlayerConeAngle;
    [SerializeField, Range(1, 100)] int roamDist;
    [SerializeField, Range(0, 10)] float roamTimer;

    // Enemy Damage
    [SerializeField] float enemyRateFire;
    [SerializeField] GameObject bullet;

    Vector3 playerDir;
    Vector3 playerDirection;
    float stoppingDistanceOriginal;
    float angleToPlayer;
    bool playerSeen;
    bool enemyShooting;

    void Start()
    {
        //gameManager.instance.UpdateGameGoal(+1);
        
    }

    void Update()
    {
        if(playerSeen && EnemySeePlayer())
        {
            
        }
    }

    int getEnemyHP()
    {
        return enemyHP;
    }

    bool EnemySeePlayer()
    {
        agent.stoppingDistance = stoppingDistanceOriginal;
        
        playerDirection = gameManager.instance.player.transform.position - enemyHeadPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDirection.x, 0, playerDirection.z), transform.forward);

        Debug.DrawRay(enemyHeadPos.position, playerDirection);
        Debug.Log(angleToPlayer);

        RaycastHit hit;

        Debug.Log(agent.remainingDistance);
        Debug.Log(gameManager.instance.player.transform.position);


        if (Physics.Raycast(enemyHeadPos.position, playerDirection, out hit))
        {
            if(hit.collider.CompareTag("Player") && angleToPlayer <= viewPlayerConeAngle)
            {
                agent.SetDestination(gameManager.instance.player.transform.position);
                

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    EnemyFacePlayer();
                }

                if(!enemyShooting)
                {
                    StartCoroutine(EnemyShooting());
                }

                return true;
            }
        }


        agent.stoppingDistance = 0;
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerSeen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerSeen = false;
        }
    }

    void EnemyFacePlayer()
    {
        Quaternion rotate = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, Time.deltaTime * facePlayerSpeed);
    }

    IEnumerator EnemyDamageFlash()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        model.material.color = Color.white;
    }

    IEnumerator EnemyShooting()
    {
        enemyShooting = true;
        Instantiate(bullet, enemyShootPos.position, transform.rotation);
        yield return new WaitForSeconds(enemyRateFire);
        enemyShooting = false;
    }

    void IDamagable.TakeDamage(int playerDmg)
    {
        enemyHP -= playerDmg;

        agent.SetDestination(gameManager.instance.player.transform.position);
        StartCoroutine(EnemyDamageFlash());

        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
