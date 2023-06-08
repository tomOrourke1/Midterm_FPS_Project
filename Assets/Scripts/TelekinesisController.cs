using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TelekinesisController : MonoBehaviour
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
    [SerializeField] float focusCost;

    [Space]
    [SerializeField][Range(0, 5)] float timeSpeed;
    [SerializeField][Range(0, 1)] float allowedDistanceToThrow;

    bool isHoldingObject;
    float timePressed;

    ITelekinesis stachedObject;

    Vector3 originalPos;


    void Start()
    {
        
    }


    void Update()
    {




        TelekinesisStart();

        PullObject();

        ReleaseObject();       

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
        if (Input.GetKeyDown(KeyCode.Mouse1) && gameManager.instance.playerscript.GetPlayerCurrentFocus() >= focusCost)
        {
            // suck item


            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;
            bool doesHit = Physics.Raycast(ray, out hit, range);

            if (doesHit)
            {
                stachedObject = hit.collider.GetComponent<ITelekinesis>();
                timePressed = 0;
                if(stachedObject != null)
                {
                    gameManager.instance.playerscript.AddFocus(-focusCost);
                    gameManager.instance.pStatsUI.UpdateValues();
                    
                    originalPos = stachedObject.GetPosition();
                    stachedObject.GetRigidbody().useGravity = false;
                }
            }
        }
    }


    void PullObject()
    {
        if (stachedObject != null && !IsObjectNull())
        {
            var pos = stachedObject.GetPosition();

            var dir = desiredPos.position - pos;

            var midPoint = pos + (dir * 0.5f);

            dir.Normalize();


            midPoint += Vector3.up * yOffset;



            Vector3 desPos = lerp2(originalPos, midPoint, desiredPos.position, Mathf.Clamp01(timePressed * timeSpeed));


            Vector3 dirToDesPos = desPos - pos;
            Debug.DrawLine(desPos, pos);


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
        if ((Input.GetKeyUp(KeyCode.Mouse1) || !Input.GetKey(KeyCode.Mouse1)) && stachedObject != null && !IsObjectNull())
        {

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
            }
            else
            {
                var vel = stachedObject.GetVelocity();

                // if the object is moving faster than the push force then cap it at the push force
                if(vel.magnitude > releaseForce)
                {
                    stachedObject.TakeVelocity(releaseForce * (vel.normalized));
                }


                

            }

            stachedObject = null;
        }
        else if (Input.GetKeyDown(KeyCode.E) && stachedObject != null)
        {
            stachedObject.TakeVelocity(stachedObject.GetVelocity() * 0.02f);
            stachedObject.GetRigidbody().useGravity = true;
            stachedObject = null;            
        }

    }




    Vector3 lerp2(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 pp = Vector3.Lerp(p0, p1, t);
        Vector3 ppp = Vector3.Lerp(p1, p2, t);

        Vector3 pppp = Vector3.Lerp(pp, ppp, t);

        return pppp;
    }



}
