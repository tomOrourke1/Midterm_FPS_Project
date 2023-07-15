using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicChannel : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] AudioSource[] source;

    private int currPlay;

    private void Start()
    {
        if (source.Length == 0)
        {
            return;
        }

        StartCoroutine(WaitToPlay());
    }

    public void PlayTrackRandom()
    {
        currPlay = Random.Range(0, source.Length);
        source[currPlay].Play();
        StartCoroutine(WaitTrackLength());
    }

    public void PlayTrack(int trackNum)
    {
        source[trackNum].Play();
        currPlay = trackNum;
    }

    private IEnumerator WaitTrackLength()
    {
        yield return new WaitForSeconds(source[currPlay].clip.length);
        StartCoroutine(WaitToPlay());
    }

    private IEnumerator WaitToPlay()
    {
        yield return new WaitForSeconds(Random.Range(1f, 10f));

        PlayTrackRandom();
    }
}
