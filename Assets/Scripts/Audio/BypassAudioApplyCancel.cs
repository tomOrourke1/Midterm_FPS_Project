using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BypassAudioApplyCancel : MonoBehaviour
{
    [Header("Apply/Cancel SFX")]
    [SerializeField] AudioSource applyBypassSource;
    [SerializeField] AudioSource cancelBypassSource;
    [SerializeField] AudioMixer masterMix;
    [SerializeField] AudioMixer applyCancelMixer;

    private void Starter()
    {
        float temp = 0f;
        masterMix.GetFloat("SFXVolume", out temp);
        applyCancelMixer.SetFloat("MixVol", temp);
    }


    public void PlayApply()
    {
        Starter();
        applyBypassSource.Play();
    }

    public void PlayCancel()
    {
        Starter();
        cancelBypassSource.Play();
    }

}
