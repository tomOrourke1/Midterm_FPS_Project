using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Player : MonoBehaviour, IDamagable
{
    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;
    [SerializeField] Camera mainCamera;

    [Header("----- Player Stats -----")]
    [SerializeField] float maxHP;
    [SerializeField] float maxFocus;
    [SerializeField] float maxShield;
    [SerializeField, Range(3, 8)] float playerSpeed;

    [Header("----- Jump Stats -----")]
    [SerializeField, Range(10, 50)] float gravityValue;
    [SerializeField, Range(8, 25)] float jumpHeight;
    [SerializeField] int jumpMax;

    [Header("----- Dash Stats -----")]
    [SerializeField] int DashMax;
    [SerializeField] float DashSpeed;
    [SerializeField] float DashDuration;
    [SerializeField] float DashCooldown;

    [Header("----- Crouch Stats -----")]
    [SerializeField] float CrouchSpeed;


    [SerializeField] float unCrouchSpeed;

    // Movement
    private Vector3 playerVelocity;
    private Vector3 move;
    private bool groundedPlayer;

    // Stats
    private float currentHP;
    private float currentShield;
    private float currentFocus;

    // Jump
    private int jumpTimes;

    // Dash
    private int currentDashes;
    private bool isDashing;
    private bool DashRecharging;

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
        currentDashes = DashMax;
        //controller = gameObject.AddComponent<CharacterController>();
        RespawnPlayer();
        currentHP = maxHP;
        currentShield = maxShield;
        currentFocus = maxFocus;

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
        if (!DashRecharging && currentDashes < DashMax)
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
        if (!isCrouching && Input.GetKeyDown(KeyCode.LeftShift) && currentDashes > 0/* && (playerVelocity.x != 0 || playerVelocity.z != 0)*/)
        {
            // This prevents dash from being called while dash is active.
            StartCoroutine(StartDash());
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

        if (isDashing)
        {

            handleDash();
        }
        else
        {
            handleWalk();
        }

        handleJump();

        // Gravity
        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
       
    }

    public void RespawnPlayer()
    {
        gameManager.instance.pStatsUI.UpdateValues();

        controller.enabled = false;
        if(gameManager.instance != null && gameManager.instance.PlayerSpawnPOS != null)
        {
            transform.position = gameManager.instance.PlayerSpawnPOS.transform.position;
        }
        controller.enabled = true;
        currentHP = maxHP;
    }

    IEnumerator StartDash()
    {
        isDashing = true;
        yield return new WaitForSeconds(DashDuration);
        currentDashes--;
        isDashing = false;
    }

    IEnumerator RechargeDash()
    {
        DashRecharging = true;
        yield return new WaitForSeconds(DashCooldown);
        currentDashes++;
        DashRecharging = false;
    }

    IEnumerator FlashDamage()
    {
        //gameManager.instance.flashDamage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        //gameManager.instance.flashDamage.gameObject.SetActive(false);
    }

    public float GetPlayerCurrentHP()
    {
        return currentHP;
    }
    public float GetPlayerCurrentShield() 
    { 
        return currentShield; 
    }
    public float GetPlayerCurrentFocus()
    {
        return currentFocus;
    }

    public float GetPlayerMaxHP()
    {
        return maxHP;
    }
    public float GetPlayerMaxFocus()
    {
        return maxFocus;
    }
    public float GetPlayerMaxShield()
    {
        return maxShield;
    }

    public void TakeDamage(int dmg)
    {

        if (currentShield <= 0)
        {
            currentHP -= dmg;
        }
        else
        {
            currentShield -= dmg;
        }
        
        gameManager.instance.pStatsUI.UpdateValues();

        // Moved this outside of the if statement because the screen
        // should always falsh red when recieving damage.
        //StartCoroutine(FlashDamage());

        if (currentHP <= 0)
        {
            // Currently the lose coroutine is not functioning
            gameManager.instance.LoseGame();
        }

    }

    void handleWalk()
    {
        // Move the player at walk speed
        controller.Move(move * Time.deltaTime * playerSpeed);
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

    void handleDash()
    {
        // Move the player at dash speed
        controller.Move(move * Time.deltaTime * DashSpeed);
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
}
