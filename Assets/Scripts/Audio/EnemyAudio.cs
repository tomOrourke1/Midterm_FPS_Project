using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [Header("Enemy Audio Source")]
    [SerializeField] AudioSource source;

    [Header("SFX")]
    [SerializeField] AudioClip[] enemyWalk;
    [SerializeField] AudioClip[] enemyShoot;
    [SerializeField] AudioClip[] enemyHurt;
    [SerializeField] AudioClip[] enemyDeath;
    [SerializeField] AudioClip disruptorClip;

    private bool playingDisruptorSFX;

    public void PlayEnemy_Walk()
    {
        source.PlayOneShot(enemyWalk[Random.Range(0, enemyWalk.Length)]);
    }

    public void PlayEnemy_Shoot()
    {
        source.PlayOneShot(enemyShoot[Random.Range(0, enemyShoot.Length)]);
    }

    public void PlayEnemy_Hurt()
    {
        source.PlayOneShot(enemyHurt[Random.Range(0, enemyHurt.Length)]);
    }

    public void PlayEnemy_Death()
    {
        // Needs to be changed to death when/if we get death sounds.
        source.PlayOneShot(enemyHurt[Random.Range(0, enemyHurt.Length)]);
    }

    public void DisruptorLoopedAudio()
    {
        if (!playingDisruptorSFX)
        {
            source.Play();
            Debug.Log("played");
            StartCoroutine(PlayDisruptor());
        }
    }

    private IEnumerator PlayDisruptor()
    {
        playingDisruptorSFX = true;
        yield return new WaitForSeconds(disruptorClip.length);
        playingDisruptorSFX = false;
    }
}
