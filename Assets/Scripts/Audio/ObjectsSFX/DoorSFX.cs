using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;

    [Header("SFX")]
    [SerializeField] AudioClip doorOpen;
    [SerializeField] AudioClip doorClose;
    [SerializeField] AudioClip doorUnlock;

    public void PlayDoor_Open()
    {
        source.PlayOneShot(doorOpen);
    }

    public void PlayDoor_Close()
    {
        source.PlayOneShot(doorClose);
    }

    public void PlayDoor_Unlock()
    {
        source.PlayOneShot(doorUnlock);
    }
}
