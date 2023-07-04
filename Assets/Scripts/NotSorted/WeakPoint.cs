using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour, IDamagable
{
    [SerializeField] float BaseDamageMultiplier = 2;
    [SerializeField] float ElectroMultiplier = 2;
    [SerializeField] float FireMultiplier = 2;
    [SerializeField] float IceMultiplier = 2;
    [SerializeField] float LaserMultiplier = 2;

    [SerializeField] GameObject parent;
    IDamagable damageable;

    private void Start()
    {
        damageable = parent.GetComponent<IDamagable>();
    }

    public float GetCurrentHealth()
    {
        return damageable.GetCurrentHealth();
    }

    public void TakeDamage(float dmg)
    {
        damageable.TakeDamage(dmg * BaseDamageMultiplier);
    }

    public void TakeElectroDamage(float dmg)
    {
        damageable.TakeElectroDamage(dmg * ElectroMultiplier);
    }

    public void TakeFireDamage(float dmg)
    {
        damageable.TakeFireDamage(dmg * FireMultiplier);
    }

    public void TakeIceDamage(float dmg)
    {
        damageable.TakeIceDamage(dmg * IceMultiplier);
    }

    public void TakeLaserDamage(float dmg)
    {
        damageable.TakeLaserDamage(dmg * LaserMultiplier);
    }
}
