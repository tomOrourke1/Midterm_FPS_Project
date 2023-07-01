using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Player Audio Source")]
    [SerializeField] AudioSource playerSource;

    [Header("Sound Effects")]
    [SerializeField] AudioClip playerWalk;
    [SerializeField] AudioClip playerDash;
    [SerializeField] AudioClip playerJump;
    [SerializeField] AudioClip playerShieldDamaged;
    [SerializeField] AudioClip playerShieldBreak;
    [SerializeField] AudioClip playerHurt;
    [SerializeField] AudioClip playerDeath;
    
    public void PlaySound_Walk()
    {
        playerSource.PlayOneShot(playerWalk);
    }

    public void PlaySound_Dash()
    {
        playerSource.PlayOneShot(playerDash);
    }

    public void PlaySound_Jump()
    {
        playerSource.PlayOneShot(playerJump);
    }

    public void PlaySound_ShieldDamaged()
    {
        playerSource.PlayOneShot(playerShieldDamaged);
    }

    public void PlaySound_ShieldBreak()
    {
        playerSource.PlayOneShot(playerShieldBreak);
    }

    public void PlaySound_Hurt()
    {
        playerSource.PlayOneShot(playerHurt);
    }

    public void PlaySound_Death()
    {
        playerSource.PlayOneShot(playerDeath);
    }
}
