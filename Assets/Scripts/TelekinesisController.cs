using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] float yOffset;

    [SerializeField][Range(0, 5)] float timeSpeed;


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





    void TelekinesisStart()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
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

                    originalPos = stachedObject.GetPosition();
                    stachedObject.GetRigidbody().useGravity = false;
                }
            }
        }
    }


    void PullObject()
    {
        if (stachedObject != null)
        {
            var pos = stachedObject.GetPosition();

            var dir = desiredPos.position - pos;

            var midPoint = pos + (dir * 0.5f);

            dir.Normalize();


            midPoint += Vector3.up * yOffset;



            Vector3 desPos = lerp2(originalPos, midPoint, desiredPos.position, Mathf.Clamp01(timePressed * timeSpeed));


            Vector3 dirToDesPos = desPos - pos;
            Debug.DrawLine(desPos, pos);


            //stachedObject.TakeVelocity(dirToDesPos);


            // account if the force is too strong
            var force = dirToDesPos * pullForce;
            var nextPos = force * Time.deltaTime + pos;
            var toNextDist = Vector3.Distance(nextPos, pos);
            var toPosDist = Vector3.Distance(desiredPos.position, pos);
            if (toNextDist > toPosDist)
            {
                force *= toPosDist / toNextDist;
                Debug.LogError("TO CLOSE TO THE POS");
            }


            stachedObject.TakeVelocity(force);




            timePressed += Time.deltaTime;
            timePressed = Mathf.Clamp01(timePressed);
        }
    }

    void ReleaseObject()
    {
        if ((Input.GetKeyUp(KeyCode.Mouse0) || !Input.GetKey(KeyCode.Mouse0)) && stachedObject != null)
        {

            stachedObject.GetRigidbody().useGravity = true;

            // if it is being held than trow it.
            // otherwise just release it

            var dist = Vector3.Distance(desiredPos.position, stachedObject.GetPosition());
            if (dist <= 0.1f)
            {
                stachedObject.TakeVelocity(cam.transform.forward * pushForce);
            }

            stachedObject = null;
        }

    }




    
    Vector3 CalculateCubicBezierPoint(Vector3 p0, Vector3 p1, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        //p += 3 * u * tt * p2;
        //p += ttt * p3;

        return p;
    }


    Vector3 lerp2(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 pp = Vector3.Lerp(p0, p1, t);
        Vector3 ppp = Vector3.Lerp(p1, p2, t);

        Vector3 pppp = Vector3.Lerp(pp, ppp, t);

        return pppp;
    }



}
