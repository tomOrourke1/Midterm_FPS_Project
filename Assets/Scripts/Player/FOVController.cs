using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FOVController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Camera cam;
    [SerializeField] Camera handCam;



    [Header("Stats")]
    [SerializeField] float setSpeed = 5;
    [SerializeField] float returnSpeed = 5;
    // nned to be able to get access to the camera and set the fov of the player when they are dashing



    [SerializeField] float origFov = 60;

    float lastFov;
    float desiredFov;
    bool fovChanging;

    float handCamFov;
    float desHandFov;
    float lastHandFov;
    bool handChanging;
    private void Start()
    {
        origFov = GameManager.instance.GOFOV();
        handCamFov = handCam.fieldOfView;
    }


    private void Update()
    {
        if (fovChanging)
        {
            cam.fieldOfView = LerpFov(cam.fieldOfView, ref desiredFov, origFov, ref fovChanging, ref lastFov);
        }
        if (handChanging)
        {
            handCam.fieldOfView = LerpFov(handCam.fieldOfView, ref desHandFov, handCamFov, ref handChanging, ref lastHandFov);
        }


    }


    public void Remp()
    {
        bool outWard = desiredFov != origFov;

        var speed = outWard ? setSpeed : returnSpeed;
        var current = cam.fieldOfView;

        var nextFov = Mathf.MoveTowards(current, desiredFov, (speed * Time.deltaTime));


        if (desiredFov < lastFov)
        {
            nextFov = Mathf.Clamp(nextFov, desiredFov, lastFov);

        }
        else
        {
            nextFov = Mathf.Clamp(nextFov, lastFov, desiredFov);
        }

        cam.fieldOfView = nextFov;


        if (cam.fieldOfView == desiredFov && nextFov != origFov)
        {
            lastFov = desiredFov;
            desiredFov = origFov;
        }
        else if (nextFov == origFov)
        {
            fovChanging = false;
        }
    }

    public float LerpFov(float current, ref float desiredFov, float originalFov, ref bool fovChanging, ref float lastFov)
    {
        bool outWard = desiredFov != originalFov;

        var speed = outWard ? setSpeed : returnSpeed;

        var nextFov = Mathf.MoveTowards(current, desiredFov, (speed * Time.deltaTime));


        if (desiredFov < lastFov)
        {
            nextFov = Mathf.Clamp(nextFov, desiredFov, lastFov);

        }
        else
        {
            nextFov = Mathf.Clamp(nextFov, lastFov, desiredFov);
        }

        //cam.fieldOfView = nextFov;


        if (current == desiredFov && nextFov != originalFov)
        {
            lastFov = desiredFov;
            desiredFov = originalFov;
        }
        else if (nextFov == originalFov)
        {
            fovChanging = false;
        }

        return nextFov;
    }



    // needs to lerp to the set value
    // then detect it's there to lerp back to the original value



    public void SetFOV(float fov)
    {
        if (fov == cam.fieldOfView)
            return;

        lastFov = cam.fieldOfView;
        fovChanging = true;
        desiredFov = fov;

        var fovDelta = fov - lastFov;

        lastHandFov = handCam.fieldOfView;
        desHandFov = handCamFov + fovDelta;
        handChanging = true;

        //        cam.fieldOfView = fov;
    }

    public void AddFOV(float fov)
    {
        if (fov == (cam.fieldOfView + fov))
            return;

        lastFov = cam.fieldOfView;
        fovChanging = true;
        desiredFov = cam.fieldOfView + fov;

        lastHandFov = handCam.fieldOfView;
        desHandFov = handCam.fieldOfView + fov;
        handChanging = true;
//        cam.fieldOfView += fov;
    }



    public void SetOrigFov(float newFov)
    {
        //if (newFov == cam.fieldOfView)
        //    return;
        Debug.Log("new fov: " + newFov);


        lastFov = cam.fieldOfView;
        fovChanging = true;
        desiredFov = origFov;
        origFov = newFov;
    }
    public float GetOrigFov()
    {
        return origFov;
    }


    public void ResetFOV()
    {
        cam.fieldOfView = origFov;
    }
}
