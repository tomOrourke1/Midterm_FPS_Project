using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerState, IApplyVelocity
{

    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform playerTransform;


    [Header("----- Player Stats -----")]
    [SerializeField, Range(3, 8)] float playerSpeed;

    [Header("----- Jump Stats -----")]
    [SerializeField, Range(10, 50)] float gravityValue;
    [SerializeField, Range(8, 25)] float jumpHeight;
    [SerializeField] int jumpMax;
    [SerializeField] float jumpPowerDivision = 2.5f;
    [SerializeField] int postGroundingFramesAllowance = 10;
    

    [Header("----- Crouch Stats -----")]
    [SerializeField] float CrouchSpeed;
    [SerializeField] float unCrouchSpeed;


    [Header("--- Velocity degregation ----")]
    [SerializeField] float airDecrese = 3;
    [SerializeField] float groundDecrese = 5;


    // Movement
    private Vector3 playerVelocity;
    private Vector3 move;
    private bool groundedPlayer;

    // Jump
    private int jumpTimes;
    int framesSinceGrounded;

    // Crouch
    private bool isCrouching;
    private float origHeight;
    private Vector3 origCamPos;
    private Vector3 crouchCameraPos;
    bool unCrouching;


    // applied velocity
    Vector3 appliedVelocity;


    // getters 
    public bool IsCrouching => isCrouching;


    private void Start()
    {
        isCrouching = false;
        crouchCameraPos = Vector3.zero;

        origHeight = controller.height;
        origCamPos = mainCamera.transform.localPosition;

        unCrouching = false;

    }


    public override void OnEnter()
    {

    }


    public override void Tick()
    {
        if (InputManager.Instance.Action.Crouch.WasPressedThisFrame())
        {
            // crouch
            isCrouching = true;
            controller.height = origHeight * 0.5f;
            unCrouching = false;
        }
        else if (isCrouching && !InputManager.Instance.Action.Crouch.IsPressed())
        {
            // crouch end
            RaycastHit hit;
            if (!Physics.SphereCast(playerTransform.position, controller.radius, playerTransform.up, out hit, controller.height * 2))
            {
                if (!unCrouching)
                {
                    mainCamera.transform.localPosition = crouchCameraPos;
                    unCrouching = true;
                }
                mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, origCamPos, Time.deltaTime * unCrouchSpeed);
                controller.height = origHeight;

                if (Vector3.Distance(mainCamera.transform.localPosition, origCamPos) <= 0.001f)
                {
                    isCrouching = false;
                    unCrouching = false;
                    mainCamera.transform.localPosition = origCamPos;
                }
            }
        }


        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
            jumpTimes = 0;

            // if hit the ground remove y velocity
            appliedVelocity = LerpClampMin(appliedVelocity, 0, -groundDecrese * Time.deltaTime);
            framesSinceGrounded = 0;
        }
        else
        {
            framesSinceGrounded++;
        }
        var inp = InputManager.Instance.Action.Move.ReadValue<Vector2>();
        move = (playerTransform.right * inp.x) + (playerTransform.forward * inp.y);

        playerVelocity.y += appliedVelocity.y;
        appliedVelocity.y = 0;
        move = (move * playerSpeed) + appliedVelocity;

        controller.Move(move * Time.deltaTime);

        if (InputManager.Instance.Action.Jump.WasPressedThisFrame())
        {
            if(groundedPlayer)
            {
                playerVelocity.y = jumpHeight;
            }
            else if(jumpTimes < jumpMax)
            {
                playerVelocity.y = jumpHeight;
                jumpTimes++;
            }
            else if(framesSinceGrounded <= postGroundingFramesAllowance)
            {
                playerVelocity.y = jumpHeight;
                framesSinceGrounded += postGroundingFramesAllowance;
            }
        }
        else if(InputManager.Instance.Action.Jump.WasPressedThisFrame() && playerVelocity.y > 0)
        {
            playerVelocity.y = playerVelocity.y / jumpPowerDivision;
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);


        appliedVelocity = LerpClampMin(appliedVelocity, 0, -airDecrese * Time.deltaTime);
    }

    public override void OnExit()
    {



    }




    public void StopAllMovement()
    {
        playerVelocity = Vector3.zero;
        move = Vector3.zero;
        controller.Move(Vector3.zero);
        appliedVelocity = Vector3.zero;
    }

    public void ApplyVelocity(Vector3 velocity)
    {

        appliedVelocity = velocity;
    }


    Vector3 LerpClampMin(Vector3 vector, float min, float change)
    {
        var dir = vector.normalized;

        var delta = dir * change;

        Vector3 nextVec = vector + delta;

        var dot = Vector3.Dot(dir, nextVec);

        var initMag = vector.magnitude;
        var nextMag = nextVec.magnitude;


        if (nextMag > initMag || dot < 0 || nextMag < min)
        {
            nextVec = dir * min;
        }


        return nextVec;
    }

    
}
