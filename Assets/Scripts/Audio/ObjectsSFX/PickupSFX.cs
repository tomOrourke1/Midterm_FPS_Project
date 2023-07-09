using UnityEngine;

public class PickupSFX : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource source;

    public void Play_OneShot()
    {
        source.Play();
        Debug.Log("played");
    }

}
