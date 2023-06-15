using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pyroBlast : KinesisBase
{
    [Header("------ Fireball Settings ------")]
    //[SerializeField] public float focusCost;
    //[SerializeField] public float fireRate;
    bool readyToFire;

    [Header("------ Fireball Components ------")]
    [SerializeField] Transform attackPoint;
    [SerializeField] Rigidbody rb;
    
   // [SerializeField] SphereCollider fireballRadius;
    [SerializeField] GameObject fireball;

    [Header("------ Force Components ------")]

    [SerializeField] float ThrowForce;
    [SerializeField] float ThrowUpwardForce;


    GameObject currentBall;

      void Start()
    {
        readyToFire = true;
    }
    void Update()
    {
        Fire();
    }


    
   public override void Fire()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1) && readyToFire == true && GameManager.instance.GetPlayerResources().Focus.CurrentValue > focusCost)
        {
        //    fireballRadius.enabled = false;
            currentBall = Instantiate(fireball, attackPoint.position, Quaternion.identity);
            //rb.useGravity = false;
            currentBall.GetComponent<Rigidbody>().useGravity = false;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && currentBall != null && readyToFire == true )
        {
            StartCoroutine(cooldown());
            GameManager.instance.GetPlayerResources().SpendFocus(focusCost);
            currentBall.GetComponent<Rigidbody>().useGravity = true;
            readyToFire = false;
         //   fireballRadius.enabled = true;

            Vector3 forceDirection = Camera.main.transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
            {
                forceDirection = (hit.point - attackPoint.position).normalized;
            }


            Vector3 forceApplied = forceDirection * ThrowForce + transform.up * ThrowUpwardForce;
            currentBall.GetComponent<Rigidbody>().AddForce(forceApplied, ForceMode.Impulse);
           
        }
        
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(fireRate);
        readyToFire = true;
    }

}
