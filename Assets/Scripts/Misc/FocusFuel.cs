using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusFuel : MonoBehaviour, IDamagable
{

    [SerializeField] Renderer render;

    [SerializeField] Material mat;
    [SerializeField] EnemyAudio source;


    Material enemyColor;

    public float GetCurrentHealth()
    {
        return 10f;
    }

    public void TakeDamage(float dmg)
    {
        StartCoroutine(FlashDamage());
        var h = GetComponents<IDamagable>();

        source.PlayEnemy_Hurt();

        foreach (var c in h)
        {
            if(c != this)
            {
                c.TakeDamage(dmg);
            }
        }
    }

    public void TakeElectroDamage(float dmg)
    {
    }

    public void TakeFireDamage(float dmg)
    {
    }

    public void TakeIceDamage(float dmg)
    {
    }

    public void TakeLaserDamage(float dmg)
    {
    }


    IEnumerator FlashDamage()
    {
        enemyColor = render.material; // saves enemy's color
        render.material = mat; // sets enemy's color to red to show damage
        yield return new WaitForSeconds(0.15f); // waits for a few seconds for the player to notice
        render.material = enemyColor; // changes enemy's color back to their previous color
    }



}
