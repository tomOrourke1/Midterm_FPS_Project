using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusFuelEnemy : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] enemyHurt;
    // Start is called before the first frame update
    bool isPlaying;
    public void PlayEnemy_Hurt()
    {
        if (!isPlaying)
        {
            StartCoroutine(wait());
            source.PlayOneShot(enemyHurt[Random.Range(0, enemyHurt.Length)]);
        }
    }


    private IEnumerator wait()
    {
        isPlaying = true;
        yield return new WaitForSeconds(waitTime);
        isPlaying = false;
    }
}
