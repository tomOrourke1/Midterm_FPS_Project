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

    [SerializeField] float totalChargeNeeded;


    public UnityEvent OnCryoHold;
    public UnityEvent OnCryoThrow;
    public UnityEvent OnCryoStop;
    public UnityEvent OnCryoForm;

    bool canFire;
    bool throwIce;

    public override void Fire()
    {
        if (InputManager.Instance.Action.Kinesis.WasPressedThisFrame() && HasFocus())
        {
         //currentSpear = Instantiate(iceSpear, attackPoint.position, Quaternion.identity);
            OnCryoHold?.Invoke();
            isCasting = true;
        }
        if (!InputManager.Instance.Action.Kinesis.IsPressed() && canFire)
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

            currentSpear = Instantiate(iceSpear, attackPoint.position, Camera.main.transform.rotation);
            Vector3 forceApplied = forceDirection * ThrowForce + transform.up * ThrowUpwardForce;
            currentSpear.GetComponent<Rigidbody>().AddForce(forceApplied, ForceMode.Impulse);
            throwIce = false;
            isCasting = false;
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

    public override void StopFire()
    {

        throwIce = false;
        isCasting = false;
        canFire = false;


        OnCryoStop?.Invoke();
    }
}
