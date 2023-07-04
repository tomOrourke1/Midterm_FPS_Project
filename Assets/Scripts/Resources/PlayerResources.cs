using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour, IDamagable, IHealReciever, IFocusReciever, IShieldReceiver
{
    [SerializeField] HealthPool health;
    [SerializeField] ShieldPool shield;
    [SerializeField] FocusPool focus;

    public HealthPool Health => health;
    public ShieldPool Shield => shield;
    public FocusPool Focus => focus;

    [SerializeField] float ShieldBreakInvulnerabilityDuration = 0.5f;

    [Header("--- camera shake values ---")]
    [SerializeField] float shakeIntensity;
    [SerializeField] float shakeSpeed;
    [SerializeField] float shakeTime;




    bool isVulnerable = true;

    private void Start()
    {
        FillAllStats();
    }

    private void OnEnable()
    {
        health.OnResourceDepleted += PlayerDied;
        health.OnResourceDecrease += FlashDamage;

        shield.OnResourceDepleted += BreakShield;
        shield.OnResourceDecrease += FlashShield;

        shield.OnResourceDepleted += UpdateShieldDepletionRate;

    }
    private void OnDisable()
    {
        health.OnResourceDepleted -= PlayerDied;
        health.OnResourceDecrease -= FlashDamage;

        shield.OnResourceDepleted -= BreakShield;
        shield.OnResourceDecrease -= FlashShield;

        shield.OnResourceDepleted -= UpdateShieldDepletionRate;
    }

    public void TakeDamage(float dmg)
    {
        if (!isVulnerable)
            return;

        if(shield.SpendResource(dmg))
        {
            //UIManager.instance.FlashPlayerShieldHit();
            //UIManager.instance.FlashBreakShield();
            CameraShaker.instance.StartShake(shakeIntensity, shakeSpeed, shakeTime);
        }
        else
        {
            CameraShaker.instance.StartShake(shakeIntensity, shakeSpeed, shakeTime);
            float diff = dmg - shield.CurrentValue;
            shield.Decrease(shield.CurrentValue);
            health.Decrease(diff);
            //UIManager.instance.FlashPlayerHealthHit();
        }
        UIManager.instance.GetPlayerStats().UpdateValues();
    }
    public void TakeIceDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeElectroDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeLaserDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    void PlayerDied()
    {
        GameManager.instance.GetPlayerScript().ResetPlayer();
        UIManager.instance.uiStateMachine.SetDeath(true);


        GameManager.instance.GetPlayerObj()?.GetComponent<CasterScript>()?.Current?.StopFire();

        // trigger lose game if the player dies
        UIManager.instance.LoseGame();
    }

    public void AddHealing(float healAmount)
    {
        health.Increase(healAmount);
        UIManager.instance.GetPlayerStats().UpdateValues();
    }

    public void AddShield(float shieldAmount)
    {
        shield.Increase(shieldAmount);
        UIManager.instance.GetPlayerStats().UpdateValues();
    }

    public bool SpendFocus(float amt)
    {
        var b = focus.SpendResource(amt);
        UIManager.instance.GetPlayerStats().UpdateValues();

        if (amt > focus.CurrentValue)
        {
            UIManager.instance.FocusDepleted();
        }

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


    private void FlashDamage()
    {
        UIManager.instance.FlashPlayerHealthHit();
    }
    private void FlashShield()
    {
        UIManager.instance.FlashPlayerShieldHit();
    }
    private void BreakShield()
    {
        UIManager.instance.FlashBreakShield();
        StartCoroutine(ShieldInvulnerability());

    }

    // I know that you hate this, I'll improve it later. For now it works.
    IEnumerator ShieldInvulnerability()
    {
        SetVulnerability(false);
        yield return new WaitForSeconds(ShieldBreakInvulnerabilityDuration);
        SetVulnerability(true);
    }

    void UpdateShieldDepletionRate()
    {
        UIManager.instance.GetPlayerStats().sliderScript.UpdtateShieldDepleationTimer();
    }



    public void MaxOutFocus()
    {
        focus.FillToMax();
        UIManager.instance.GetPlayerStats().UpdateValues();
    }

    public float GetCurrentHealth()
    {
        return health.CurrentValue;
    }

    /// <summary>
    /// true = take damage
    /// false = don't take damage
    /// </summary>
    /// <param name="isVulnerable"></param>
    public void SetVulnerability(bool isVulnerable)
    {
        this.isVulnerable = isVulnerable;
    }

}
