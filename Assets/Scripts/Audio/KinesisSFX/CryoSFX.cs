using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryoSFX : MonoBehaviour
{
    [Header("Kinesis Audio Source")]
    [SerializeField] AudioSource kinesisSouce;

    [Header("Cryo SFX")]
    [SerializeField] AudioClip cryoForm;
    [SerializeField] AudioClip cryoHold;
    [SerializeField] AudioClip cryoHit;

    public void PlayCryo_Form()
    {
        kinesisSouce.PlayOneShot(cryoForm);
    }

    public void PlayCryo_Hold()
    {
        kinesisSouce.PlayOneShot(cryoHold);
    }

    public void PlayCryo_Hit()
    {
        kinesisSouce.PlayOneShot(cryoHit);
    }
}
