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

    // Start is called before the first frame update
    void Start()
    {
        readyToFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }
    public override void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && readyToFire == true && GameManager.instance.GetPlayerResources().Focus.CurrentValue > focusCost)
        {
           
            currentSpear = Instantiate(iceSpear, attackPoint.position, Quaternion.identity);
           
            
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && currentSpear != null && readyToFire == true)
        {
            StartCoroutine(cooldown());
            GameManager.instance.GetPlayerResources().SpendFocus(focusCost);
       
            readyToFire = false;
            //   fireballRadius.enabled = true;

            Vector3 forceDirection = Camera.main.transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
            {
                forceDirection = (hit.point - attackPoint.position).normalized;
            }


            Vector3 forceApplied = forceDirection * ThrowForce + transform.up * ThrowUpwardForce;
            currentSpear.GetComponent<Rigidbody>().AddForce(forceApplied, ForceMode.Impulse);

        }
    }
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(fireRate);
        readyToFire = true;
    }
}