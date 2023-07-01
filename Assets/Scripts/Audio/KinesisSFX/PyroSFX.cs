using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyroSFX : MonoBehaviour
{
    [Header("Kinesis Audio Source")]
    [SerializeField] AudioSource kinesisSouce;
    
    [Header("Pyro SFX")]
    [SerializeField] AudioClip pyroForm;
    [SerializeField] AudioClip pyroHold;
    [SerializeField] AudioClip pyroLaunch;
    [SerializeField] AudioClip pyroExplode;

    public void PlayPyro_Forming()
    {
        kinesisSouce.PlayOneShot(pyroForm);
    }

    public void PlayPyro_Hold()
    {
        kinesisSouce.PlayOneShot(pyroHold);
    }

    public void PlayPyro_Launch()
    {
        kinesisSouce.PlayOneShot(pyroLaunch);
    }

    public void PlayPyro_Explode()
    {
        kinesisSouce.PlayOneShot(pyroExplode);
    }
}
