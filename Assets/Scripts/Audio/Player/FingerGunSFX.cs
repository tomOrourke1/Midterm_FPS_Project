using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerGunSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource source2;

    [Header("SFX")]
    [SerializeField] AudioClip shootSFX;
    [SerializeField] AudioClip hitEnemySFX;

    public void Play_OneShot()
    {
        source.PlayOneShot(shootSFX);
    }

    public void PlayOneShot_HitEnemy()
    {
        source2.PlayOneShot(hitEnemySFX);
    }
}
