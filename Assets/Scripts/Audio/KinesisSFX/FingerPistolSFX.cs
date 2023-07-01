using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinesisAudio : MonoBehaviour
{
    [Header("Kinesis Audio Source")]
    [SerializeField] AudioSource kinesisSouce;

    [Header("Finger Gun SFX")]
    [SerializeField] AudioClip fingerGunShoot;
    [SerializeField] AudioClip fingerGunHit;

    public void PlayFingerPistol_Shoot()
    {
        kinesisSouce.PlayOneShot(fingerGunShoot);
    }

    public void PlayFingerPistol_Hit()
    {
        kinesisSouce.PlayOneShot(fingerGunHit);
    }
}
