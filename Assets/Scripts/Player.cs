using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Player Stats -----")]
    [SerializeField] int HP;
    [SerializeField] int Focus;
    [Range(3, 8)][SerializeField] float playerSpeed;
    [Range(10, 50)][SerializeField] float gravityValue;
    [Range(8, 25)][SerializeField] float jumpHeight;
    [SerializeField] int jumpMax;
    [SerializeField] float DashSpeed;
    [SerializeField] float DashDuration;
    [SerializeField] float DashCooldown;

    private Vector3 playerVelocity;
    private Vector3 move;
    private int jumpTimes;
    private bool groundedPlayer;
    private bool isDashing;
    private bool dashReady;

    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
        dashReady = true;
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetButtonDown("Fire3") && dashReady/* && (playerVelocity.x != 0 || playerVelocity.z != 0)*/)
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
            controller.Move(move * Time.deltaTime * DashSpeed);
        }
        else
        {
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
        StartCoroutine(DashCooldownTimer());
        isDashing = false;
    }

    IEnumerator DashCooldownTimer()
    {
        dashReady = false;
        yield return new WaitForSeconds(DashCooldown);
        dashReady = true;
    }

}
