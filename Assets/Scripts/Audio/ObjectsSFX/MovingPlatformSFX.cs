using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;

    [Header("SFX")]
    [SerializeField] AudioClip movingPlatformSound;

    public void PlayMovingPlatform_Sound()
    {
        source.PlayOneShot(movingPlatformSound);
    }
}
