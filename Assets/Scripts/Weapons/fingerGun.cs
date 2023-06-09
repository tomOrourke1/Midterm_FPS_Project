using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerGun : MonoBehaviour 
   
{
    [Header("----- Finger Gun Settings -----")]

    [SerializeField] int shootDamage;
    [SerializeField] float fireRate;
    [SerializeField] int shootDist;
    [SerializeField] int speed;
    [SerializeField] GameObject fingerBullet;
    [SerializeField] Transform shootPos;


    [SerializeField] float focusAmount;

    private bool isShooting;



    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && !isShooting)
        {
            StartCoroutine(shoot());
            //Instantiate(fingerBullet, shootPos.position, shootPos.rotation);
        }
    }
    
    IEnumerator shoot()
    {

        isShooting = true;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f,0.5f)), out hit, shootDist))
        {
            IDamagable damageable = hit.collider.GetComponent<IDamagable>();

            if(damageable != null)
            {
                damageable.TakeDamage(shootDamage);
                gameManager.instance.playerscript.AddFocus(focusAmount);
                gameManager.instance.pStatsUI.UpdateValues();
                
            }

            

        }

        yield return new WaitForSeconds(fireRate);
        isShooting=false;
    }


  
   
}
