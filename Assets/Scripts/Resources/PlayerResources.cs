using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour, IDamagable, IHealReciever, IFocusReciever
{
    [SerializeField] HealthPool health;
    [SerializeField] ShieldPool shield;
    [SerializeField] FocusPool focus;

    public HealthPool Health => health;
    public ShieldPool Shield => shield;
    public FocusPool Focus => focus;


    private void Start()
    {
        FillAllStats();
    }

    

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
        gameManager.instance.pStatsUI.UpdateValues();
    }

    void PlayerDied()
    {
        // trigger lose game if the player dies
        gameManager.instance.LoseGame();
    }

    public void AddHealing(float healAmount)
    {
        health.Increase(healAmount);
        gameManager.instance.pStatsUI.UpdateValues();
    }

    public bool SpendFocus(float amt)
    {
        var b = focus.SpendResource(amt);
        gameManager.instance.pStatsUI.UpdateValues();
        return b;
    }

    public void AddFocus(float amt)
    {
        focus.Increase(amt);
        gameManager.instance.pStatsUI.UpdateValues();
    }


    public void FillAllStats()
    {
        health.FillToMax();
        shield.FillToMax();
        focus.FillToMax();
    }
}
