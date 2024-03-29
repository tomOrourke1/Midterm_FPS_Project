using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        if (InputManager.Instance.Action.Kinesis.WasPressedThisFrame() && !HasFocus())
        {
            UIManager.instance.FocusDepleted();
            return;
        }

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
            //base.EnableOpenRadial();
        }
        if (canActivate)
        {
           
            canActivate = false;
            GameManager.instance.GetPlayerResources().SpendFocus(focusCost);
            forceDirection = Camera.main.transform.forward;
            Vector3 velocity = (forceDirection * force) + (transform.up * upwardForce);// * Time.fixedDeltaTime;

            var hits = Physics.SphereCastAll(Camera.main.transform.position, windRadius, Camera.main.transform.forward, windRange);

            var zero = Physics.OverlapSphere(Camera.main.transform.position, windRadius);


            foreach(var z in zero)
            {
                bool wallHit = false;
                if(!z.CompareTag("Player"))
                {
                    var dir = (z.transform.position + Vector3.up) /*z.ClosestPoint(Camera.main.transform.position)*/ - Camera.main.transform.position;
                    var rayHits = Physics.RaycastAll(Camera.main.transform.position, dir, windRadius);
                    foreach(var hit in rayHits)
                    {
                        IApplyVelocity vel = hit.collider.gameObject.GetComponent<IApplyVelocity>();
                        if(vel == null)
                        {
                            wallHit = true;
                            break;
                        }
                    }
                    var applyVel = z.GetComponent<IApplyVelocity>();
                    if(applyVel != null && !wallHit)
                    {
                        applyVel.ApplyVelocity(velocity);
                    }
                }
            }
            


            foreach (var currentHit in hits)
            {
              bool wallHit = false;
                if (!currentHit.collider.CompareTag("Player"))
                {
                    var dir = (currentHit.transform.position + Vector3.up) - Camera.main.transform.position;
                    RaycastHit[] rayHits = Physics.RaycastAll(Camera.main.transform.position, dir, windRange);
                    foreach (var hit in rayHits)
                    {
                        IApplyVelocity vel = hit.collider.gameObject.GetComponent<IApplyVelocity>();
                        if (vel == null )
                        {
                          wallHit = true;
                            break;
                        }
                    }
                    var applyVel = currentHit.collider.GetComponent<IApplyVelocity>();
                    if (applyVel != null && wallHit == false)
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
