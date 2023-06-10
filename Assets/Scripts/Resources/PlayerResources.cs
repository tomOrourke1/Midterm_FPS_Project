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
            var shieldBreak = shield.AtMin() ?
                StartCoroutine(UIManager.instance.GetFlashDamageScript().CrackShieldDisplay()) :
                StartCoroutine(UIManager.instance.GetFlashDamageScript().FlashShieldDisplay());
        }
        else
        {
            float diff = dmg - shield.CurrentValue;
            shield.Decrease(shield.CurrentValue);
            health.Decrease(diff);
            StartCoroutine(UIManager.instance.GetFlashDamageScript().FlashDamageDisplay());

        }
        UIManager.instance.GetPlayerStats().UpdateValues();
    }

    void PlayerDied()
    {
        // trigger lose game if the player dies
        UIManager.instance.LoseGame();
    }

    public void AddHealing(float healAmount)
    {
        health.Increase(healAmount);
        UIManager.instance.GetPlayerStats().UpdateValues();
    }

    public bool SpendFocus(float amt)
    {
        var b = focus.SpendResource(amt);
        UIManager.instance.GetPlayerStats().UpdateValues();
        return b;
    }

    public void AddFocus(float amt)
    {
        focus.Increase(amt);
        UIManager.instance.GetPlayerStats().UpdateValues();
    }


    public void FillAllStats()
    {
        health.FillToMax();
        shield.FillToMax();
        focus.FillToMax();
    }
}
