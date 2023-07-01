using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{

    [Header("---- Components -----")]
    [SerializeField] Camera cam;


    [Header("stats")]
    [SerializeField] float rotInSpeed;
    [SerializeField] float rotOutSpeed;


    float angle;
    float angleDestination;
    bool rotating;

    private void LateUpdate()
    {
       // Camera.main.transform;
        if (rotating)
        {
            var euler = cam.transform.localRotation.eulerAngles;
            // this needs to rotate the z axis back to zero
            // this should hapen over a time / speed period
            
            var current = (angle < 0 && euler.z > 0) ? (euler.z - 360) : euler.z;
            
            
            var speed = angleDestination != 0 ? rotOutSpeed : rotInSpeed;
            //var x = Mathf.MoveTowardsAngle(current, angleDestination, speed * Time.deltaTime);
            var x = Mathf.MoveTowards(current, angleDestination, speed * Time.deltaTime);
            

            if (angle < 0)
            {                
                x = Mathf.Clamp(x, angle, 0);
            }
            else if(angle > 0)
            {
                x = Mathf.Clamp(x, 0, angle);
            }


            euler.z = x;

            cam.transform.localRotation = Quaternion.Euler(euler);

            if (x == angleDestination && angleDestination != 0)
            {
                angleDestination = 0;
            }
            else if (x == 0)
            {
                rotating = false;
            }

        }
       
    }



    // needs to have tilt out which should be faster 
    // then a tilt in which should be slightly slower


    public void StartTilt(float tiltAmount)
    {
        if (tiltAmount == cam.transform.localRotation.eulerAngles.z)    // might need to be == 0 instead?
            return;


        angle = tiltAmount;
        angleDestination = tiltAmount;
        rotating = true;

    }

    public void ResetTilt()
    {
        // set members back
        angle = 0;
        angleDestination = 0;
        rotating = false;

        // set rotation back
        var e = cam.transform.localRotation.eulerAngles;
        e.z = 0;
        cam.transform.localRotation = Quaternion.Euler(e);
    }


}
