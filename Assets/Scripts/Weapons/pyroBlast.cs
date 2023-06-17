using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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



    public UnityEvent OnFireHold;
    public UnityEvent OnFireThrow;


    bool isReady;
    bool canActivate;


    public override void Fire()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasFocus())
        {
            OnFireHold?.Invoke();
            //    fireballRadius.enabled = false;
            //rb.useGravity = false;
        }
        if (!Input.GetKey(KeyCode.Mouse1) && isReady)
        {
            OnFireThrow?.Invoke();
            isReady = false;
        }
        if (canActivate)
        {
            canActivate = false;
            var currentBall = Instantiate(fireball, attackPoint.position, Quaternion.identity);
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
            currentBall.GetComponent<Rigidbody>().AddForce(forceApplied, ForceMode.Impulse);

        }

    }


    bool HasFocus()
    {
        return GameManager.instance.GetPlayerResources().Focus.CurrentValue >= focusCost;
    }


    public void SetIsReady(bool red)
    {
        isReady = red;
    }

    public void SetCanActive(bool act)
    {
        canActivate = act;
    }



}
