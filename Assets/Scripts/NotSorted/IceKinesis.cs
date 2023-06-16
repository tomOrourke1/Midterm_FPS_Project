using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKinesis : KinesisBase
{
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject iceSpear;
    GameObject currentSpear;

    bool readyToFire;



    [SerializeField] float ThrowForce;
    [SerializeField] float ThrowUpwardForce;

    float totalCharge;
    [SerializeField] float totalChargeNeeded;

    // Start is called before the first frame update
    void Start()
    {
        readyToFire = true;
    }

    // Update is called once per frame
    
    public override void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && readyToFire == true && GameManager.instance.GetPlayerResources().Focus.CurrentValue > focusCost)
        {
            currentSpear = Instantiate(iceSpear, attackPoint.position, Quaternion.identity);        
        }
        if (Input.GetKey(KeyCode.Mouse1) && readyToFire == true && GameManager.instance.GetPlayerResources().Focus.CurrentValue > focusCost)
        {
            totalCharge += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && currentSpear != null && readyToFire == true)
        {

            if (totalCharge >= totalChargeNeeded)
            {
                StartCoroutine(cooldown());
                GameManager.instance.GetPlayerResources().SpendFocus(focusCost);

                readyToFire = false;


                Vector3 forceDirection = Camera.main.transform.forward;

                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
                {
                    forceDirection = (hit.point - attackPoint.position).normalized;
                }


                Vector3 forceApplied = forceDirection * ThrowForce + transform.up * ThrowUpwardForce;
                currentSpear.GetComponent<Rigidbody>().AddForce(forceApplied, ForceMode.Impulse);
                totalCharge = 0;
            }
            else { Destroy(currentSpear); }           

        }
    }
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(fireRate);
        readyToFire = true;
    }
}
