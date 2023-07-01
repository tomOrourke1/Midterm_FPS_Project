using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [Header("--- Components ----")]
    [SerializeField] PlayerStateMachine statemachine;
    [SerializeField] Camera cam;
    [SerializeField] CharacterController controller;
    [SerializeField] TiltController tiltController;
    [SerializeField] FOVController fovController;

    [Header("--- States ---")]
    [SerializeField] PlayerMovementState moveState;

    [Header("--- dash colldowns ---")]
    [SerializeField] PlayerCooldownBehaviour dash1;
    [SerializeField] PlayerCooldownBehaviour dash2;
    [SerializeField] PlayerCooldownBehaviour dashCooldown;


    public void ResetPlayer()
    {
        // reset movement
        moveState.StopAllMovement();
        statemachine.ResetState();

        //reset camera stuff
        tiltController.ResetTilt();
        fovController.ResetFOV();

    }

    public void RespawnPlayer()
    {
        controller.enabled = false;
        if(GameManager.instance != null && GameManager.instance.GetPlayerSpawnPOS() != null)
        {
            var tran = GameManager.instance.GetPlayerSpawnPOS().transform;
            transform.position = tran.position;
            var forward = tran.forward;
            forward.y = 0;
            forward.Normalize();
            transform.rotation = Quaternion.LookRotation(forward);
        }
        controller.enabled = true;

        GameManager.instance.GetKeyChain().Clear();

        UpdatePlayerStats();
    }

    void UpdatePlayerStats()
    {
        // this might need to also reset the number of dashes
        dash1.ResetCooldown();
        dash2.ResetCooldown();
        dashCooldown.ResetCooldown();

        GameManager.instance.GetPlayerResources().FillAllStats();
        UIManager.instance.GetPlayerStats().UpdateValues();
    }
}
