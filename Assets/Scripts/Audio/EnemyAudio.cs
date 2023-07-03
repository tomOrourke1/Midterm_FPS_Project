using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [Header("Enemy Audio Source")]
    [SerializeField] AudioSource source;

    [Header("SFX")]
    [SerializeField] AudioClip enemyWalk;
    [SerializeField] AudioClip enemyShoot;
    [SerializeField] AudioClip enemyHurt;
    [SerializeField] AudioClip enemyDeath;

   
    public void PlayEnemy_Walk()
    {
        source.PlayOneShot(enemyWalk);
    }

    public void PlayEnemy_Shoot()
    {
        source.PlayOneShot(enemyShoot);
    }

    public void PlayEnemy_Hurt()
    {
        source.PlayOneShot(enemyHurt);
    }

    public void PlayEnemy_Death()
    {
        source.PlayOneShot(enemyDeath);
    }

}
