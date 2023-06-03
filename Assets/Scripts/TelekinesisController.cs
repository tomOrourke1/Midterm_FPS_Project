using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisController : MonoBehaviour
{

    [Header("----- Components -----")]
    [SerializeField] Camera cam;

    [Header("----- stats ------")]
    [SerializeField] float range;
    [SerializeField] int pullForce;
    [SerializeField] int pushForce;


    bool isHoldingObject;


    ITelekinesis stachedObject;


    void Start()
    {
        
    }


    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            // suck item

            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;
            bool doesHit = Physics.Raycast(ray, out hit, range);

            if(doesHit)
            {
                stachedObject = hit.collider.GetComponent<ITelekinesis>();


            }


        }


        if(stachedObject != null)
        {
            var pos = stachedObject.GetPosition();

            var dir = transform.position - pos;

            dir.Normalize();

            var rb = stachedObject.GetRigidbody();

            rb.AddForce(dir * pullForce, ForceMode.Force);


        }



        if(Input.GetKeyUp(KeyCode.Mouse0) && stachedObject != null)
        {


            // if it is being held than trow it.
            // otherwise just release it



            stachedObject = null;
        }




    }








}
