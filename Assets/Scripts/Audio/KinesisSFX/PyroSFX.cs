using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyroSFX : MonoBehaviour
{
    [Header("Kinesis Audio Source")]
    [SerializeField] AudioSource kinesisSouce;
    [SerializeField] AudioSource holdSource;

    [Header("Pyro SFX")]
    [SerializeField] AudioClip pyroForm;
    [SerializeField] AudioClip pyroHold;
    [SerializeField] AudioClip pyroLaunch;
    [SerializeField] AudioClip pyroExplode;

    public void PlayPyro_Forming()
    {
        holdSource.Stop();
        kinesisSouce.PlayOneShot(pyroForm);
    }

    public void PlayPyro_Hold()
    {
        holdSource.Play();
    }

    public void PlayPyro_Launch()
    {
        holdSource.Stop();
        kinesisSouce.PlayOneShot(pyroLaunch);
    }

    public void PlayPyro_Explode()
    {
        holdSource.Stop();
        kinesisSouce.PlayOneShot(pyroExplode);
    }

    public void ForceStopHoldSource()
    {
        holdSource.Stop();
    }
}
