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


    private Vector3 playerVelocity;
    private Vector3 move;
    private float currentHP;
    private float currentShield;
    private float currentFocus;
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
        //controller = gameObject.AddComponent<CharacterController>();
        RespawnPlayer();
        currentHP = maxHP;
        currentShield = maxShield;
        currentFocus = maxFocus;
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

        // If pressing left shift and there are dashes available
        // then dash
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

    IEnumerator Dash()
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
        currentHP -= dmg;
        
        // Moved this outside of the if statement because the screen
        // should always falsh red when recieving damage.
        //StartCoroutine(FlashDamage());

        if (currentHP <= 0)
        {
            // Currently the lose coroutine is not functioning
            gameManager.instance.LoseGame();
        }
    }

    IEnumerator FlashDamage()
    {
        //gameManager.instance.flashDamage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        //gameManager.instance.flashDamage.gameObject.SetActive(false);
    }

}
