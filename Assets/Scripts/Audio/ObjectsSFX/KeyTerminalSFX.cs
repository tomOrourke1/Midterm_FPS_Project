using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTerminalSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;

    [Header("SFX")]
    [SerializeField] AudioClip acceptedCard;
    [SerializeField] AudioClip noKeys;

    public void PlayOneShot_Accepted()
    {
        source.PlayOneShot(acceptedCard);
    }

    public void PlayOneShot_NoKeys()
    {
        source.PlayOneShot(noKeys);
    }

}
