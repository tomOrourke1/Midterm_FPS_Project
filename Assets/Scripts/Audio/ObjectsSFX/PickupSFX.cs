using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;

    [Header("SFX")]
    [SerializeField] AudioClip pickupSFX;

    public void PlayDoor_Open()
    {
        source.PlayOneShot(pickupSFX);
    }

}
