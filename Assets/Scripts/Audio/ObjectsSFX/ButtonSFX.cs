using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;

    [Header("SFX")]
    [SerializeField] AudioClip buttonPressed;

    public void PlayButton_Pressed()
    {
        source.PlayOneShot(buttonPressed);
    }
}
