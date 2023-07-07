using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TelekinesisController : KinesisBase
{

    [Header("----- Components -----")]
    [SerializeField] Camera cam;
    [SerializeField] Transform desiredPos; 

    [Header("----- stats ------")]
    [SerializeField] float range;
    [SerializeField] int pullForce;
    [SerializeField] int pushForce;
    [SerializeField] int releaseForce;

    [SerializeField] int rotationSpeed;

    [SerializeField] float yOffset;


    [Space]
    [SerializeField][Range(0, 5)] float timeSpeed;
    [SerializeField][Range(0, 1)] float allowedDistanceToThrow;


    [Header("--- Events ----")]
    public UnityEvent OnTeleStart;
    public UnityEvent OnTelePush;
    public UnityEvent OnTeleStopped;

    bool isHoldingObject;
    float timePressed;

    //ITelekinesis stachedObject;
    MoveableObject stachedObject;

    Vector3 originalPos;

    void Start()
    {

    }




    private void FixedUpdate()
    {
        
    }



    bool IsObjectNull()
    {
        var obj = stachedObject as UnityEngine.Object;
        
        return obj == null;
    }

    void TelekinesisStart()
    {
        if (InputManager.Instance.Action.Kinesis.WasPressedThisFrame())
        {
          
            // suck item

            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;
            bool doesHit = Physics.Raycast(ray, out hit, range);

            if (doesHit)
            {
                // Changed this from an interface to a script
                stachedObject = hit.collider.GetComponent<MoveableObject>();
                timePressed = 0;
                if(stachedObject != null && GameManager.instance.GetPlayerResources().SpendFocus(focusCost))
                {
                    isCasting = true;

                    OnTeleStart?.Invoke();
                    originalPos = stachedObject.GetPosition();
                    stachedObject.GetRigidbody().useGravity = false;
                    //Debug.Log("Here");
                    stachedObject.SetVolitile(true);
                }
                else
                {
                    stachedObject = null;
                }
            }
        }
    }

    // Helper function so that I don't muck up your code
    void SetRotation()
    {
        Vector3 desiredRotation = Camera.main.transform.forward;

//        stachedObject.transform.forward = Vector3.Lerp(desiredRotation, stachedObject.transform.forward, Mathf.Clamp01(timePressed * timeSpeed));
        var desRot = Quaternion.LookRotation(desiredRotation);
        stachedObject.transform.rotation = Quaternion.Lerp(stachedObject.transform.rotation, desRot, Time.deltaTime * 10);

    }

    void PullObject()
    {
        if (stachedObject != null && !IsObjectNull())
        {
            base.DisableOpenRadial();
            float distToPos = Vector3.Distance(stachedObject.transform.position, desiredPos.position);
            //Debug.Log(stachedObject.GetVolitile());
            if (distToPos <= 1)
            {
                stachedObject.SetVolitile(false); 
                //Debug.Log("Check");
            }

            SetRotation();

            var pos = stachedObject.GetPosition();

            var dir = desiredPos.position - pos;

            var midPoint = pos + (dir * 0.5f);

            dir.Normalize();


            midPoint += Vector3.up * yOffset;

     

            Vector3 desPos = lerp2(originalPos, midPoint, desiredPos.position, Mathf.Clamp01(timePressed * timeSpeed));


            Vector3 dirToDesPos = desPos - pos;
            //Debug.DrawLine(desPos, pos);


            // account if the force is too strong
            var force = dirToDesPos * pullForce;
            var nextPos = force * Time.deltaTime + pos;
            var toNextDist = Vector3.Distance(nextPos, pos);
            var toPosDist = Vector3.Distance(desiredPos.position, pos);
            if (toNextDist > toPosDist)
            {
                force *= toPosDist / toNextDist;
            }


            stachedObject.TakeVelocity(force);



            // rotation

            var rb = stachedObject.GetRigidbody();


            //rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.LookRotation(cam.transform.forward), Time.deltaTime * rotationSpeed);
            //rb.angularVelocity = (Vector3.Cross(Vector3.up, rb.transform.up) + Vector3.Cross(cam.transform.forward, rb.transform.forward) ).normalized * rotationSpeed;
            rb.angularVelocity = Vector3.zero;

            timePressed += Time.deltaTime;
            timePressed = Mathf.Clamp01(timePressed);
        }
    }

    void ReleaseObject()
    {
        if ((InputManager.Instance.Action.Kinesis.WasReleasedThisFrame() || !InputManager.Instance.Action.Kinesis.IsPressed()) && stachedObject != null && !IsObjectNull())
        {
            // Causes the object to become volitile when throw begins
            stachedObject.SetVolitile(true);


            isCasting = false;
            OnTelePush?.Invoke();

            stachedObject.GetRigidbody().useGravity = true;

            // if it is being held than trow it.
            // otherwise just release it

            var dist = Vector3.Distance(desiredPos.position, stachedObject.GetPosition());
            if (dist <= allowedDistanceToThrow)
            {
                RaycastHit hit;
                bool does = Physics.Raycast(cam.ViewportPointToRay(new Vector3(0.5f, 0.5f)), out hit, 100);
                Vector3 dir;
                if(does)
                {
                    dir = hit.point - stachedObject.GetPosition();
                }
                else
                {
                    dir = (cam.transform.forward * 100 + cam.transform.position) - stachedObject.GetPosition();
                }

                stachedObject.TakeVelocity(dir.normalized * pushForce);
                base.EnableOpenRadial();
            }
            else
            {
                var vel = stachedObject.GetVelocity();

                // if the object is moving faster than the push force then cap it at the push force
                if(vel.magnitude > releaseForce)
                {
                    stachedObject.TakeVelocity(releaseForce * (vel.normalized));
                }

                base.EnableOpenRadial();
                

            }

            stachedObject = null;
        }
        else if (InputManager.Instance.Action.Interact.WasPressedThisFrame() && stachedObject != null)
        {
            stachedObject.TakeVelocity(stachedObject.GetVelocity() * 0.02f);
            stachedObject.GetRigidbody().useGravity = true;
            stachedObject = null;

            StopFire();
        }

    }

    public override void Fire()
    {
        TelekinesisStart();

        PullObject();

        ReleaseObject();
    }


    Vector3 lerp2(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 pp = Vector3.Lerp(p0, p1, t);
        Vector3 ppp = Vector3.Lerp(p1, p2, t);

        Vector3 pppp = Vector3.Lerp(pp, ppp, t);

        return pppp;
    }

    public override void StopFire()
    {
        isCasting = false;

        stachedObject = null;

        OnTeleStopped?.Invoke();
    }
}
