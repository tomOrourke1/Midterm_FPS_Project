using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;

    public void Play_Explosion()
    {
        source.transform.parent = null;
        source.PlayOneShot(clip);
        Destroy(source, clip.length);
    }
}
