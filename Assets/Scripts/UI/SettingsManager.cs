using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] AudioMixer masterMix;

    public void SetVolume(float vol)
    {
        masterMix.SetFloat("volume", vol);
    }


}
