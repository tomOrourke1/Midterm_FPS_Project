using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class pyroBlast : KinesisBase
{
    [Header("------ Fireball Settings ------")]
    //[SerializeField] public float focusCost;
    //[SerializeField] public float fireRate;

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
    public UnityEvent OnFireStop;
    [SerializeField] GameObject fireFocusParticles;


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
            base.DisableOpenRadial();
            OnFireHold?.Invoke();
            fireFocusParticles.SetActive(true);
            //    fireballRadius.enabled = false;
            //rb.useGravity = false;
            isCasting = true;
        }
        if (!InputManager.Instance.Action.Kinesis.IsPressed() && isReady)
        {
            base.DisableOpenRadial();
            OnFireThrow?.Invoke();
            isReady = false;
            fireFocusParticles.SetActive(false);
        }
        if (canActivate)
        {
            canActivate = false;
            var currentBall = Instantiate(fireball, attackPoint.position, Quaternion.identity);
            GameManager.instance.GetPlayerResources().SpendFocus(focusCost);
            //readyToFire = false;
            //   fireballRadius.enabled = true;

            Vector3 forceDirection = Camera.main.transform.forward;





            var aimValue = GameManager.instance.GetAimAssistValue();
            var isOn = GameManager.instance.GetSettingsManager().settings.aimAssistEnabled;
            bool regFire = true;


            if (isOn)
            {
                RaycastHit hit;
                var doHIt = Physics.SphereCast(Camera.main.transform.position, aimValue, Camera.main.transform.forward, out hit);



                var doHItAll = Physics.SphereCastAll(Camera.main.transform.position, aimValue, Camera.main.transform.forward, 100f);
                if (doHItAll.Length > 0)
                {
                    List<RaycastHit> resul = new();
                    foreach (var c in doHItAll)
                    {
                        if (c.collider?.GetComponent<IDamagable>() != null && !c.collider.CompareTag("Player"))
                        {
                            resul.Add(c);
                        }
                    }
                    if (resul.Count > 0)
                    {
                        RaycastHit closest = resul[0];
                        foreach (var r in resul)
                        {
                            if (r.distance < closest.distance)
                            {
                                closest = r;
                            }
                        }
                        forceDirection = (closest.point - attackPoint.position).normalized;
                        regFire = false;
                    }
                    else
                    {
                        regFire = true;
                    }
                }
                else
                {
                    regFire = true;
                }






                //if (doHIt)
                //{

                //    IDamagable damageable = hit.collider.GetComponent<IDamagable>();

                //    if (damageable != null)
                //    {
                //        forceDirection = (hit.point - attackPoint.position).normalized;
                //    }
                //}
                //else
                //{
                //    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
                //    {
                //        forceDirection = (hit.point - attackPoint.position).normalized;
                //    }
                //}

            }


            if (regFire)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
                {
                    forceDirection = (hit.point - attackPoint.position).normalized;
                }

            }



            Vector3 forceApplied = forceDirection * ThrowForce + transform.up * ThrowUpwardForce;
            currentBall.GetComponent<Rigidbody>().AddForce(forceApplied, ForceMode.Impulse);

            fireFocusParticles.SetActive(false);

            isCasting = false;
            base.EnableOpenRadial();
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

    public override void StopFire()
    {
        base.EnableOpenRadial();
        isReady = false;
        isCasting = false;
        canActivate = false;

        OnFireStop?.Invoke();
    }
}
