using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryChargeIntake : MonoBehaviour, IDamagable
{
    [SerializeField] ChargeableBattery battery;

    public float GetCurrentHealth()
    {
        return battery.GetMaxCharge();
    }

    public void TakeDamage(float dmg)
    {
        
    }

    public void TakeElectroDamage(float dmg)
    {
       battery.AddCharge();
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
}
