using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;

    [Header("SFX")]
    [SerializeField] AudioClip laserSound;

    public void PlayLaser_Sound()
    {
        source.PlayOneShot(laserSound);
    }
}
