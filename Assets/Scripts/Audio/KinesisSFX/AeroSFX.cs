using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeroSFX : MonoBehaviour
{
    [Header("Kinesis Audio Source")]
    [SerializeField] AudioSource kinesisSouce;

    [Header("Aero SFX")]
    [SerializeField] AudioClip aeroForm;
    [SerializeField] AudioClip aeroPush;

    public void PlayAero_Form()
    {
        kinesisSouce.PlayOneShot(aeroForm);
    }

    public void PlayAero_Push()
    {
        kinesisSouce.PlayOneShot(aeroPush);
    }
}
