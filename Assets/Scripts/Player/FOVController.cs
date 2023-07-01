using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Camera cam;



    [Header("Stats")]
    [SerializeField] float returnSpeed = 5;
    // nned to be able to get access to the camera and set the fov of the player when they are dashing



    [SerializeField] float origFov = 60;

    private void Start()
    {
    }


    private void Update()
    {
        if (cam.fieldOfView != origFov)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, origFov, returnSpeed * Time.deltaTime);
        }

    }




    public void SetFOV(float fov)
    {
        cam.fieldOfView = fov;
    }

    public void AddFOV(float fov)
    {
        cam.fieldOfView += fov;
    }



    public void SetOrigFov(float newFov)
    {
        origFov = newFov;
    }
    public float GetOrigFov()
    {
        return origFov;
    }

}
