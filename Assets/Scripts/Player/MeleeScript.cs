using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MeleeScript : MonoBehaviour
{

    [Header("----- Knife Settings -----")]

    [SerializeField] float knifeDamage;
    [SerializeField] float attackRate;
    [SerializeField] float knifeRange;
    [SerializeField] float knifeRadius;

    // This was implemented as a fix to sphere cast ignoring any colliders that are in the sphere cast's initial position
    [Header("----- Small Cast -----")]
    [SerializeField] float smallCastRange;
    [SerializeField] float smallCastRadius;


    private bool isKnifing;


    public UnityEvent OnKnife;


    bool canKnife;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnKnife?.Invoke();
            //StartCoroutine(Knife());
            //Instantiate(fingerBullet, shootPos.position, shootPos.rotation);
        }

        if(canKnife)
        {
            canKnife = false;
            AnimationKnife();
        }
    }

    //private void OnDrawGizmos()
    //{
    //    //RaycastHit hit;
    //    //Physics.SphereCast(Camera.main.transform.position, knifeRadius, Camera.main.transform.forward, out hit, knifeRange);

    //    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    //    Gizmos.DrawWireSphere(ray.GetPoint(knifeRange), knifeRadius);

    //}


    void AnimationKnife()
    {
        RaycastHit hit;

        if (Physics.SphereCast(Camera.main.transform.position, smallCastRadius, Camera.main.transform.forward, out hit, smallCastRange))
        {
            IDamagable damageable = hit.collider.GetComponent<IDamagable>();
            DamageCollider(damageable);
        }
        else if (Physics.SphereCast(Camera.main.transform.position, knifeRadius, Camera.main.transform.forward, out hit, knifeRange))
        {
            IDamagable damageable = hit.collider.GetComponent<IDamagable>();
            DamageCollider(damageable);
        }

    }



    IEnumerator Knife()
    {

        isKnifing = true;
        RaycastHit hit;

        if (Physics.SphereCast(Camera.main.transform.position, smallCastRadius, Camera.main.transform.forward, out hit, smallCastRange))
        {
            IDamagable damageable = hit.collider.GetComponent<IDamagable>();
            DamageCollider(damageable);
        }
        else if (Physics.SphereCast(Camera.main.transform.position, knifeRadius, Camera.main.transform.forward, out hit, knifeRange))
        {
            IDamagable damageable = hit.collider.GetComponent<IDamagable>();
            DamageCollider(damageable);
        }

        yield return new WaitForSeconds(attackRate);
        isKnifing = false;
    }

    void DamageCollider(IDamagable damageable)
    {
        if (damageable != null)
        {
            // This will give the player max focus whenever they kill an enemy.
            // I am currently waiting for a way to get enemies health or to detect their death.

            //if (hit.collider.GetComponent<EnemyAI>().GetEnemyHP() <= knifeDamage)
            //{
            //    gameManager.instance.playerscript.AddFocus(maxFocus);
            //    gameManager.instance.pStatsUI.UpdateValues();
            //}

            damageable.TakeDamage(knifeDamage);
        }
    }


    public void SetCanknife()
    {
        canKnife = true;
    }

}
