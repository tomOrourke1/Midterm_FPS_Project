using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReciever : MonoBehaviour, IDamagable
{
    [SerializeField] HealthPool health;
    [SerializeField] ShieldPool shield;


    private void OnEnable()
    {
        health.OnResourceDepleted += PlayerDied;
    }
    private void OnDisable()
    {
        health.OnResourceDepleted -= PlayerDied;
    }


    public void TakeDamage(float dmg)
    {
        if(shield.SpendResource(dmg))
        {
        }
        else
        {
            float diff = dmg - shield.CurrentValue;
            shield.Decrease(shield.CurrentValue);
            health.Decrease(diff);
        }
    }

    void PlayerDied()
    {
        // trigger lose game if the player dies
        gameManager.instance.LoseGame();
    }
}
