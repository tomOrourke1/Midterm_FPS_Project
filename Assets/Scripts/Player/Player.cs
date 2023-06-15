using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;
    [SerializeField] Camera mainCamera;

    [Header("----- Player Stats -----")]
    [SerializeField, Range(3, 8)] float playerSpeed;

    [Header("----- Jump Stats -----")]
    [SerializeField, Range(10, 50)] float gravityValue;
    [SerializeField, Range(8, 25)] float jumpHeight;
    [SerializeField] int jumpMax;

    [Header("----- Dash Stats -----")]
    [SerializeField] int maxDashes;
    [SerializeField, Range(0, 1)] float DashSpeed;
    [SerializeField, Range(.01f, 1)] float DashDuration;
    [SerializeField] float DashCooldown;
    [SerializeField, Range(.01f, 20)] float tiltAmount;

    [Header("----- Crouch Stats -----")]
    [SerializeField] float CrouchSpeed;
    [SerializeField] float unCrouchSpeed;


    // Movement
    private Vector3 playerVelocity;
    private Vector3 move;
    private bool groundedPlayer;


    // Jump
    private int jumpTimes;

    // Dash
    private int currentDashes;
    private bool isDashing;
    private bool DashRecharging;
    Vector3 dashDir;
    float newTilt;
    float origTilt;
    float origFov;
    float dashFovZoom;

    // Crouch
    private bool isCrouching;
    private float origHeight;
    private Vector3 origCamPos;
    private Vector3 crouchCameraPos;
    bool unCrouching;



    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
        isCrouching = false;
        DashRecharging = false;
        origCamPos = mainCamera.transform.localPosition;
        crouchCameraPos = new Vector3(0,0,0);
        origHeight = controller.height;
        currentDashes = maxDashes;
        origFov = Camera.main.fieldOfView;

        //controller = gameObject.AddComponent<CharacterController>();
        RespawnPlayer();
        UIManager.instance.GetPlayerStats().UpdateValues();

        unCrouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        // If dash is not recharging and there are less dashes than the max
        // then start recharging dashes
        if (!DashRecharging && currentDashes < maxDashes)
        {
            StartCoroutine(RechargeDash());
        }

        // If crouch is being held while the player is grounded
        // then crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            handleCrouch();
        } 
        else if (isCrouching && !Input.GetKey(KeyCode.LeftControl))
        {
            handleCrouchEnd();
        }

        // If pressing left shift and there are dashes available
        // then dash
        if (!isCrouching && !isDashing && Input.GetKeyDown(KeyCode.LeftShift) && currentDashes > 0 && move.normalized.magnitude > 0.5f)
        {
            // This prevents dash from being called while dash is active.
            StartCoroutine(StartDash(move));
        }

        // Resets the jumps when player lands
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpTimes = 0;
        }


        // Sets vector for movement
        move = (transform.right * Input.GetAxis("Horizontal") + (transform.forward * Input.GetAxis("Vertical")));

        checkVertical();
        checkHorizontal();

        handleWalk();
        handleJump();

        // Return field of view to normal
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, origFov, Time.deltaTime * 5);

        // Gravity
        playerVelocity.y -= gravityValue * Time.deltaTime;

        // Move
        controller.Move(playerVelocity * Time.deltaTime);
        
       
    }

    void checkHorizontal()
    {
        //float zRotation = Mathf.Clamp(Camera.main.transform.rotation.z, , );

        //transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (Input.GetAxis("Horizontal") == 0)
        {
            newTilt = origTilt;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            newTilt = origTilt - tiltAmount;
        }
        else
        {
            newTilt = origTilt + tiltAmount;
        }
    }

    void checkVertical()
    {
        if (Input.GetAxis("Vertical") == 0)
        {
            dashFovZoom = origFov;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            dashFovZoom = 50;
        }
        else
        {
            dashFovZoom = 70;
        }
    }

    public void RespawnPlayer()
    {
        controller.enabled = false;
        if(GameManager.instance != null && GameManager.instance.GetPlayerSpawnPOS() != null)
        {
            var tra = GameManager.instance.GetPlayerSpawnPOS().transform;
            transform.position = tra.position;
            var forward = tra.forward;
            forward.y = 0;
            forward.Normalize();
            transform.rotation = Quaternion.LookRotation(forward);
        }
        controller.enabled = true;

        // Reset the player's keys
        GameManager.instance.GetKeyChain().Clear();

        UpdatePlayerStats();    // this updates the player dashes when the player respawns.

    }

    //IEnumerator dash(Vector3 dir)
    //{
    //    isDashing = true;

    //    Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomDir, Time.deltaTime * 100);
    //    //Camera.main.gameObject.transform.rotation = Quaternion.Lerp(Camera.main.gameObject.transform.rotation, tiltNew, Time.deltaTime * .5f);

    //    dashDir += dir * dashSpeed;
    //    yield return new WaitForSeconds(.1f);
    //    dashDir = Vector3.zero;
    //    isDashing = false;
    //}

    IEnumerator StartDash(Vector3 dir)
    {
        isDashing = true;

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, dashFovZoom, Time.deltaTime * 100);
        mainCamera.GetComponent<CameraController>().DashCam(newTilt, origTilt - tiltAmount, origTilt + tiltAmount);
        dashDir += dir * DashSpeed;

        yield return new WaitForSeconds(DashDuration);

        currentDashes--;
        dashDir = Vector3.zero;

        isDashing = false;
    }

    IEnumerator RechargeDash()
    {
        DashRecharging = true;
        yield return new WaitForSeconds(DashCooldown);
        currentDashes++;
        DashRecharging = false;
    }

    void handleWalk()
    {
        // Move the player at walk speed
        controller.Move(dashDir + move * Time.deltaTime * playerSpeed);
    }

    void handleJump()
    {
        // Changes the height position of the player.
        if (Input.GetButtonDown("Jump") && jumpTimes < jumpMax)
        {
            jumpTimes++;
            playerVelocity.y = jumpHeight;
        }
    }

    void handleCrouch()
    {
        // Here we will crouch
        Debug.Log("Crouch");
        isCrouching = true;
        controller.height = origHeight * .5f;
        unCrouching = false;
    }

    void handleCrouchEnd()
    {
        // Here we will uncrouch
        Debug.Log("Uncrouch");

        RaycastHit Hit;

        if (!Physics.SphereCast(transform.position, controller.radius, transform.up, out Hit, controller.height * 2))
        {
            if(!unCrouching)
            {
                mainCamera.transform.localPosition = crouchCameraPos;
                unCrouching = true;
            }

            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, origCamPos, Time.deltaTime * unCrouchSpeed);
            controller.height = origHeight;

            if(Vector3.Distance(mainCamera.transform.localPosition, origCamPos) <= 0.001f)
            {
                isCrouching = false;
                unCrouching = false;
            }
        }
    }

    void UpdatePlayerStats()
    {
        currentDashes = maxDashes;
        jumpTimes = 0;
        GameManager.instance.GetPlayerResources().FillAllStats();

        UIManager.instance.GetPlayerStats().UpdateValues();
    }
}
