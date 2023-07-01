using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;

    [Header("SFX")]
    [SerializeField] AudioClip explosion;

    public void Play_Explosion()
    {
        source.PlayOneShot(explosion);
    }
}
