using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IceKinesis : KinesisBase
{
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject iceSpear;
    GameObject currentSpear;



    [SerializeField] float ThrowForce;
    [SerializeField] float ThrowUpwardForce;

    float totalCharge;
    [SerializeField] float totalChargeNeeded;


    public UnityEvent OnCryoHold;
    public UnityEvent OnCryoThrow;
    public UnityEvent OnCryoStop;


    bool canFire;

    bool throwIce;

    public override void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && HasFocus())
        {
         //currentSpear = Instantiate(iceSpear, attackPoint.position, Quaternion.identity);
            OnCryoHold?.Invoke();
        }
        if (!Input.GetKey(KeyCode.Mouse1) && canFire)
        {
            OnCryoThrow?.Invoke();
            canFire = false;
        }

        if(throwIce)
        {
            GameManager.instance.GetPlayerResources().SpendFocus(focusCost);

            Vector3 forceDirection = Camera.main.transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
            {
                forceDirection = (hit.point - attackPoint.position).normalized;
            }

            currentSpear = Instantiate(iceSpear, attackPoint.position, Quaternion.identity);
            Vector3 forceApplied = forceDirection * ThrowForce + transform.up * ThrowUpwardForce;
            currentSpear.GetComponent<Rigidbody>().AddForce(forceApplied, ForceMode.Impulse);
            totalCharge = 0;
            throwIce = false;
        }


    }

    bool HasFocus()
    {
        return GameManager.instance.GetPlayerResources().Focus.CurrentValue >= focusCost;
    }


    public void SetCanFire(bool canFire)
    {
        this.canFire = canFire;
    }

    public void ThrowIce()
    {
        throwIce = true;
    }

}
