using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scientist : MonoBehaviour, IDamagable
{
    [SerializeField] Renderer model;
    [SerializeField] float runAwayDist = 20.0f;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject key;

    [SerializeField] float scientistHP;

    bool playerSeen;
    Color scientistColor;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        scientistColor = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlay = Vector3.Distance(transform.position, gameManager.instance.player.transform.position);

        if(distanceFromPlay < runAwayDist)
        {
            Vector3 playerDir = transform.position - gameManager.instance.player.transform.position;

            Vector3 newPos = transform.position + playerDir;

            agent.SetDestination(newPos);
        }
    }

    void IDamagable.TakeDamage(float playerDmg)
    {
        scientistHP -= playerDmg;

        agent.SetDestination(gameManager.instance.player.transform.position);
        StartCoroutine(EnemyDamageFlash());

        if (scientistHP <= 0)
        {
           if(key != null)
                Instantiate(key, transform.position, transform.rotation);
            // Kevin CME BCAK HERE gameManager.instance.UpdateGameGoal(-1);
            Destroy(gameObject);
        }
    }

    IEnumerator EnemyDamageFlash()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        model.material.color = scientistColor;
    }
}
