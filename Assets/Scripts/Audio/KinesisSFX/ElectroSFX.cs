using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroSFX : MonoBehaviour
{
    [Header("Kinesis Audio Source")]
    [SerializeField] AudioSource kinesisSouce;

    [Header("Electro SFX")]
    [SerializeField] AudioClip electroCast;
    [SerializeField] AudioClip electroShoot;
    [SerializeField] AudioClip electroHit;

    public void PlayElectro_Cast()
    {
        kinesisSouce.PlayOneShot(electroCast);
    }

    public void PlayElectro_Shoot()
    {
        kinesisSouce.PlayOneShot(electroShoot);
    }

    public void PlayElectro_Hit()
    {
        kinesisSouce.PlayOneShot(electroHit);
    }

}
