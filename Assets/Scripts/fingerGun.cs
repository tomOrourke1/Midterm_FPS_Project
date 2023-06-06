using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerGun : MonoBehaviour 
   
{
    [Header("----- Finger Gun Settings -----")]

    [SerializeField] int shootDamage;
    [SerializeField] int fireRate;
    [SerializeField] int shootDist;
    [SerializeField] int speed;
    [SerializeField] GameObject fingerBullet;
    [SerializeField] Transform shootPos;

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
            IDamage damageable = hit.collider.GetComponent<IDamage>();

            if(damageable != null)
            {
                damageable.takeDamage(shootDamage);
            }
        }

        yield return new WaitForSeconds(fireRate);
        isShooting=false;
    }


  
   
}
