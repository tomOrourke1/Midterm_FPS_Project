using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    [Header("---- Components ---")]
    [SerializeField] CharacterController controller;
    [SerializeField] Transform playerTransform;
    [SerializeField] FOVController fovController;
    [SerializeField] TiltController tiltController;

    [Header("----- Dash Stats -----")]
    [SerializeField, Range(0, 50)] float DashSpeed;
    [SerializeField, Range(.01f, 1)] float DashDuration;
    [SerializeField, Range(.01f, 20)] float tiltAmount;
    [SerializeField, Range(0, 30)] float dashFovZoomAmount;


    // private members
    Vector3 dashDir;
    float timeInsideState;



    public override void OnEnter()
    {
        var inpV = Input.GetAxis("Vertical");
        var inpH = Input.GetAxis("Horizontal");
        dashDir = (transform.right * inpH + (transform.forward * inpV));

        if(dashDir.magnitude == 0)
        {
            dashDir = playerTransform.forward;
        }

        // x being if it is forward or backward
        var x = Mathf.Round(inpV) * -1;
        var y = Mathf.Round(inpH) * -1;

        // if the dash is going forward but not right or left
        if (x == 0 && y == 0)
            x = 1;


        fovController.SetFOV(fovController.GetOrigFov() + (dashFovZoomAmount * x));

        tiltController.StartTilt(tiltAmount * y);


        timeInsideState = 0;
    }

    public override void Tick()
    {
        controller.Move(dashDir);
        timeInsideState += Time.deltaTime;
    }

    public override void OnExit()
    {
    }

    public override bool ExitCondition()
    {
        return timeInsideState >= DashDuration;
    }







}
