using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamagable, IEntity
{
    [Header("Enemy Stats")]
    [SerializeField] float enemyHP;
    [SerializeField] int enemySpeed;
    [SerializeField] int turnSpeed;
    [SerializeField] int peripheralAngle;
    [SerializeField, Range(1, 100)] int roamDist;
    [SerializeField, Range(0, 10)] float roamTimer;
    
    [Header("Enemy Components")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer enemyMeshRenderer;
    [SerializeField] Transform enemyHeadPos;
    [SerializeField] Transform enemyShootPos;

    [Header("Enemy Weapon Info")]
    [SerializeField] float enemyRateFire;
    [SerializeField] GameObject bullet;

    Color enemyColor;
    Vector3 startingPos;
    Vector3 playerDirection;
    float stoppingDistanceOriginal;
    float angleToPlayer;
    bool playerSeen;
    bool enemyShooting;
    bool destinationChosen;

    void Start()
    {
        enemyColor = enemyMeshRenderer.material.color;
        startingPos = transform.position;
        stoppingDistanceOriginal = agent.stoppingDistance;
    }

    void Update()
    {
        if (playerSeen && !EnemySeePlayer())
            StartCoroutine(Roam());
        else if (agent.destination != GameManager.instance.GetPlayerObj().transform.position)
            StartCoroutine(Roam());
    }

    public float GetEnemyHP()
    {
        return enemyHP;
    }

    IEnumerator Roam()
    {
        if (!destinationChosen && agent.remainingDistance < 0.05f)
        {
            destinationChosen = true;
            agent.stoppingDistance = 0;

            yield return new WaitForSeconds(roamTimer);
            destinationChosen = false;

            Vector3 randomPos = Random.insideUnitSphere * roamDist;
            randomPos += startingPos;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomPos, out hit, roamDist, 1);

            agent.SetDestination(hit.position);
        }
    }

    bool EnemySeePlayer()
    {
        agent.stoppingDistance = stoppingDistanceOriginal;

        playerDirection = GameManager.instance.GetPlayerObj().transform.position - enemyHeadPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDirection.x, 0, playerDirection.z), transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(enemyHeadPos.position, playerDirection, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= peripheralAngle)
            {
                agent.SetDestination(GameManager.instance.GetPlayerObj().transform.position);

                if (agent.remainingDistance <= agent.stoppingDistance)
                    EnemyFacePlayer();

                if (!enemyShooting)
                    StartCoroutine(EnemyShooting());

                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerSeen = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerSeen = false;
    }

    void EnemyFacePlayer()
    {
        Quaternion rotate = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, Time.deltaTime * turnSpeed);
    }

    IEnumerator EnemyDamageFlash()
    {
        enemyMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        enemyMeshRenderer.material.color = enemyColor;
    }

    IEnumerator EnemyShooting()
    {
        enemyShooting = true;
        EnemyFacePlayer();

        var dirToPlayer = GameManager.instance.GetPlayerObj().transform.position - enemyShootPos.position;
        dirToPlayer.Normalize();

        Instantiate(bullet, enemyShootPos.position, Quaternion.LookRotation(dirToPlayer));
        yield return new WaitForSeconds(enemyRateFire);
        enemyShooting = false;
    }

    public void TakeDamage(float playerDmg)
    {
        enemyHP -= playerDmg;

        agent.SetDestination(GameManager.instance.GetPlayerObj().transform.position);
        StartCoroutine(EnemyDamageFlash());

        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeIceDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeElectroDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public float GetCurrentHealth()
    {
        return enemyHP;
    }

    public void Respawn()
    {
        Destroy(gameObject);
    }
}
