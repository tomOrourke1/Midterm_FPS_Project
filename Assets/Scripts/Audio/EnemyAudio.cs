using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAudio : MonoBehaviour
{
    [Header("Enemy Audio Source")]
    [SerializeField] AudioSource shootSource;
    [SerializeField] AudioSource hurtSource;

    [Header("SFX")]
    [SerializeField] AudioClip[] enemyWalk;
    [SerializeField] AudioClip[] enemyShoot;
    [SerializeField] AudioClip[] enemyHurt;
    [SerializeField] AudioClip[] enemyDeath;

    private bool playingDisruptorSFX;

    public void PlayEnemy_Walk()
    {
        if (enemyWalk.Length > 0)
            shootSource.PlayOneShot(enemyWalk[Random.Range(0, enemyWalk.Length)]);
    }

    public void PlayEnemy_Shoot()
    {
        if (enemyShoot.Length > 0)
            shootSource.PlayOneShot(enemyShoot[Random.Range(0, enemyShoot.Length)]);
    }

    public void PlayEnemy_Hurt()
    {
        if (enemyHurt.Length > 0)
            hurtSource.PlayOneShot(enemyHurt[Random.Range(0, enemyHurt.Length)]);
    }

    public void PlayEnemy_Death()
    {
        if (enemyDeath.Length > 0)
            hurtSource.PlayOneShot(enemyDeath[Random.Range(0, enemyDeath.Length)]);
    }
}
