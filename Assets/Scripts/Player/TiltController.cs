using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{

    [Header("---- Components -----")]
    [SerializeField] Camera cam;


    [Header("stats")]
    [SerializeField] float rotSpeed;


    float angle;


    private void Update()
    {
        var euler = cam.transform.localRotation.eulerAngles;
        if (euler.z != 0)
        {
            // this needs to rotate the z axis back to zero
            // this should hapen over a time / speed period

            var x = Mathf.MoveTowards(euler.z, 0, rotSpeed * Time.deltaTime);
            if( angle < 0)
            {
                x = Mathf.Clamp(x, angle, 0);
            }
            else
            {
                x = Mathf.Clamp(x, 0, angle);
            }

            euler.z = x;

            cam.transform.localRotation = Quaternion.Euler(euler);
        }
    }







    public void StartTilt(float tiltAmount)
    {
        angle = tiltAmount;
        cam.transform.Rotate(Vector3.forward, tiltAmount);
    }


}
