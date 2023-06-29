using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerState
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



    [Header("----- Crouch Stats -----")]
    [SerializeField] float CrouchSpeed;
    [SerializeField] float unCrouchSpeed;




    // Movement
    private Vector3 playerVelocity;
    private Vector3 move;
    private bool groundedPlayer;

    // Jump
    private int jumpTimes;


    // Crouch
    private bool isCrouching;
    private float origHeight;
    private Vector3 origCamPos;
    private Vector3 crouchCameraPos;
    bool unCrouching;



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
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            // crouch
            isCrouching = true;
            controller.height = origHeight * 0.5f;
            unCrouching = false;
        }
        else if (isCrouching && !Input.GetKey(KeyCode.LeftControl))
        {
            // crouch end
            RaycastHit hit;
            if(!Physics.SphereCast(playerTransform.position, controller.radius, playerTransform.up, out hit, controller.height * 2))
            {
                if(!unCrouching)
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
        if(groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
            jumpTimes = 0;
        }

        move = (playerTransform.right * Input.GetAxis("Horizontal")) + (playerTransform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if(Input.GetKeyDown(KeyCode.Space) && jumpTimes < jumpMax)
        {
            jumpTimes++;
            playerVelocity.y = jumpHeight;
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);



    }

    public override void OnExit()
    {



    }


}
