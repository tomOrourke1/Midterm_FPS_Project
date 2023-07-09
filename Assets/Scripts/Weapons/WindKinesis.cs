using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.HID;

public class WindKinesis : KinesisBase
{
    [Header("--- Stats ----")]
    [SerializeField] float windRange;
    [SerializeField] float windRadius;
    Vector3 forceDirection;
    [SerializeField] float force;
    [SerializeField] float upwardForce;

    [Header("--- Events ----")]
    public UnityEvent OnAeroStart;
    public UnityEvent OnAeroPush;
    public UnityEvent OnAeroStopped;

    bool isReady;
    bool canActivate;

    public override void Fire()
    {



        if (InputManager.Instance.Action.Kinesis.WasPressedThisFrame() && HasFocus())
        {
            OnAeroStart?.Invoke();
            isCasting = true;
            base.DisableOpenRadial();
        }
        if (!InputManager.Instance.Action.Kinesis.IsPressed() && isReady)
        {
            OnAeroPush?.Invoke();
            isReady = false ;
            base.EnableOpenRadial();
        }
        if (canActivate)
        {
           
            canActivate = false;
            GameManager.instance.GetPlayerResources().SpendFocus(focusCost);
            forceDirection = Camera.main.transform.forward;
            Vector3 velocity = forceDirection * force + transform.up * upwardForce;

            var hits = Physics.SphereCastAll(Camera.main.transform.position, windRadius, Camera.main.transform.forward, windRange);

            foreach (var currentHit in hits)
            {
                if (!currentHit.collider.CompareTag("Player"))
                {
                    var applyVel = currentHit.collider.GetComponent<IApplyVelocity>();

                    if (applyVel != null)
                    {
                        applyVel.ApplyVelocity(velocity);
                    }
                }

            }
            isCasting = false;
            base.EnableOpenRadial();
        }



    }
    public void SetIsReady(bool red)
    {
        isReady = red;
    }

    public void SetCanActive(bool act)
    {
        canActivate = act;
    }
    public override void StopFire()
    {
        isCasting = false;
        isReady = false;
        canActivate = false;
        OnAeroStopped?.Invoke();
        base.EnableOpenRadial();

    }
    bool HasFocus()
    {
        return GameManager.instance.GetPlayerResources().Focus.CurrentValue >= focusCost;
    }
}
