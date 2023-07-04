using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerGunSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;

    [Header("SFX")]
    [SerializeField] AudioClip sfx;

    public void Play_OneShot()
    {
        source.PlayOneShot(sfx);
    }

}
