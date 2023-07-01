using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableCubeSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;

    [Header("SFX")]
    [SerializeField] AudioClip hitSurface;

    public void PlayMoveableObject_HitSurface()
    {
        source.PlayOneShot(hitSurface);
    }
}
