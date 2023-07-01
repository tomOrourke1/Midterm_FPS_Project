using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [Header("Enemy Audio Source")]
    [SerializeField] AudioSource enemySource;

    [Header("SFX")]
    [SerializeField] AudioClip enemyWalk;
    [SerializeField] AudioClip enemyShoot;
    [SerializeField] AudioClip enemyHurt;
    [SerializeField] AudioClip enemyDeath;

    public void PlayEnemy_Walk()
    {
        enemySource.PlayOneShot(enemyWalk);
    }

    public void PlayEnemy_Shoot()
    {
        enemySource.PlayOneShot(enemyShoot);
    }

    public void PlayEnemy_Hurt()
    {
        enemySource.PlayOneShot(enemyHurt);
    }

    public void PlayEnemy_Death()
    {
        enemySource.PlayOneShot(enemyDeath);
    }

}
