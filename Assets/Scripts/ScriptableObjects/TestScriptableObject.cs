using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/enemies", order = 1)]
public class TestScriptableObject : ScriptableObject, IDamagable
{
    public float health;
    public float speed;
    public float tirdness;

    public float GetCurrentHealth()
    {
        return health;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
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
}
