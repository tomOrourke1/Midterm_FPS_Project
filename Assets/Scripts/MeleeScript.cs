using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{

    [Header("----- Knife Settings -----")]

    [SerializeField] float knifeDamage;
    [SerializeField] float attackRate;
    [SerializeField] float knifeRange;
    [SerializeField] float knifeRadius;


    private bool isKnifing;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && !isKnifing)
        {
            StartCoroutine(Knife());
            //Instantiate(fingerBullet, shootPos.position, shootPos.rotation);
        }
    }

    private void OnDrawGizmos()
    {
        //RaycastHit hit;
        //Physics.SphereCast(Camera.main.transform.position, knifeRadius, Camera.main.transform.forward, out hit, knifeRange);

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Gizmos.DrawWireSphere(ray.GetPoint(knifeRange), knifeRadius);
    }

    IEnumerator Knife()
    {

        isKnifing = true;
        RaycastHit hit;
        float maxFocus = gameManager.instance.playerscript.GetPlayerMaxFocus();

        if (Physics.SphereCast(Camera.main.transform.position, knifeRadius, Camera.main.transform.forward, out hit, knifeRange))
        {

            IDamagable damageable = hit.collider.GetComponent<IDamagable>();

            if (damageable != null)
            {
                if (hit.collider.GetComponent<EnemyAI>().GetEnemyHP() <= knifeDamage)
                {
                    gameManager.instance.playerscript.AddFocus(maxFocus);
                    gameManager.instance.pStatsUI.UpdateValues();
                }

                damageable.TakeDamage((int) knifeDamage);
            }
        }

        yield return new WaitForSeconds(attackRate);
        isKnifing = false;
    }
}
