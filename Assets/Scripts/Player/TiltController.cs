using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{

    [Header("---- Components -----")]
    [SerializeField] Camera cam;








    private void Update()
    {
        if(cam.transform.localRotation.eulerAngles.z != 0)
        {
            
        }
    }







    public void StartTilt(float tiltAmount)
    {
        cam.transform.Rotate(Vector3.forward, tiltAmount);
    }


}
