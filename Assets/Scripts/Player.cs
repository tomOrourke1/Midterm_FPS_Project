using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Player Stats -----")]
    [SerializeField] float maxHP;
    [SerializeField] float maxFocus;
    // 6/5/2023 - Kevin W.
    // Added currentHP and currentFocus 
    // Stores the current HP so we can keep track of the max and current seperately
    [SerializeField] private float currentHP;
    [SerializeField]private float currentFocus;
    [SerializeField, Range(3, 8)] float playerSpeed;
    [SerializeField, Range(10, 50)] float gravityValue;
    [SerializeField, Range(8, 25)] float jumpHeight;
    [SerializeField] int jumpMax;
    [SerializeField] int DashMax;
    [SerializeField] float DashSpeed;
    [SerializeField] float DashDuration;
    [SerializeField] float DashCooldown;

    
    private Vector3 playerVelocity;
    private Vector3 move;
    private int jumpTimes;
    private int currentDashes;
    private bool groundedPlayer;
    private bool isDashing;
    private bool DashRecharging;

    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
        DashRecharging = false;
        currentDashes = DashMax;
        controller = gameObject.AddComponent<CharacterController>();

        // 6/5/2023 - Kevin W.
        // Added values so they can be updated properly.
        // When taking damage refer to currentHP as the variable to change. 
        currentHP = maxHP;
        currentFocus = maxFocus;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (!DashRecharging && currentDashes < DashMax)
        {
            StartCoroutine(ReachargeDash());
        }

        if (Input.GetButtonDown("Fire3") && currentDashes > 0/* && (playerVelocity.x != 0 || playerVelocity.z != 0)*/)
        {
            StartCoroutine(Dash());
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpTimes = 0;
        }
        
        move = (transform.right * Input.GetAxis("Horizontal") + (transform.forward * Input.GetAxis("Vertical")));

        if (isDashing)
        {
            // Move the player at dash speed
            controller.Move(move * Time.deltaTime * DashSpeed);
        }
        else
        {
            // Move the player at walk speed
            controller.Move(move * Time.deltaTime * playerSpeed);
        }

        // Changes the height position of the player.
        if (Input.GetButtonDown("Jump") && jumpTimes < jumpMax)
        {
            jumpTimes++;
            playerVelocity.y = jumpHeight;
        }

       
        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
       
    }

    IEnumerator Dash()
    {
        isDashing = true;
        yield return new WaitForSeconds(DashDuration);
        currentDashes--;
        isDashing = false;
    }

    IEnumerator ReachargeDash()
    {
        DashRecharging = true;
        yield return new WaitForSeconds(DashCooldown);
        currentDashes++;
        DashRecharging = false;
    }

    // 6/5/2023 - Additions made by Kevin W.
    // Introduced these functions to allow for the UI
    // to grab the stats easily without interfering with them
    public float GetPlayerCurrentHP()
    {
        return currentHP;
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

}
