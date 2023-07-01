using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleSFX : MonoBehaviour
{
    [Header("Kinesis Audio Source")]
    [SerializeField] AudioSource kinesisSouce;
    
    [Header("Tele SFX")]
    [SerializeField] AudioClip telePull;
    [SerializeField] AudioClip telePush;

    public void PlayTele_Pull()
    {
        kinesisSouce.PlayOneShot(telePull);
    }

    public void PlayTele_Push()
    {
        kinesisSouce.PlayOneShot(telePush);
    }
}
