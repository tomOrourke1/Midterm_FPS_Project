using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scientist : MonoBehaviour, IDamagable
{
    [Header("Scientist Stats")]
    [SerializeField] float scientistHP;
    [SerializeField] float runAwayDist = 20.0f;
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject key;

    // Color Component for when the scientist gets damaged
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
        float distanceFromPlay = Vector3.Distance(transform.position, GameManager.instance.GetPlayerObj().transform.position);

        if(distanceFromPlay < runAwayDist)
        {
            Vector3 playerDir = transform.position - GameManager.instance.GetPlayerObj().transform.position;

            Vector3 newPos = transform.position + playerDir;

            agent.SetDestination(newPos);
        }
    }

    public void TakeDamage(float playerDmg)
    {
        scientistHP -= playerDmg;

        agent.SetDestination(GameManager.instance.GetPlayerObj().transform.position);
        StartCoroutine(EnemyDamageFlash());

        if (scientistHP <= 0)
        {
           if(key != null)
                Instantiate(key, transform.position, transform.rotation);
            // Kevin CME BCAK HERE gameManager.instance.UpdateGameGoal(-1);
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
    public void TakeLaserDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    IEnumerator EnemyDamageFlash()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        model.material.color = scientistColor;
    }

    public float GetCurrentHealth()
    {
        return scientistHP;
    }
}
