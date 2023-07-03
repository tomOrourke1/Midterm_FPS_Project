using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuMusic : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] AudioSource source;

    [Header("Tracks")]
    [SerializeField] AudioClip[] musicTracks;

    private int currentTrack;

    private void Start()
    {
        if (musicTracks.Length == 0)
        {
            Debug.LogError("No Tracks in list. Please Add a track to start the music process.");
            return;
        }

        StartCoroutine(WaitToPlay());
    }

    public void PlayTrackRandom()
    {
        currentTrack = Random.Range(0, musicTracks.Length);
        source.PlayOneShot(musicTracks[currentTrack]);
        StartCoroutine(WaitTrackLength());
    }

    public void PlayTrack(int trackNum)
    {
        source.PlayOneShot(musicTracks[trackNum]);
        currentTrack = trackNum;
    }

    private IEnumerator WaitTrackLength()
    {
        yield return new WaitForSeconds(musicTracks[currentTrack].length);
        StartCoroutine(WaitToPlay());
    }

    private IEnumerator WaitToPlay()
    {
        yield return new WaitForSeconds(Random.Range(1f, 10f));

        PlayTrackRandom();
    }

}
