using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float dmg);

    float GetCurrentHealth();

    void TakeIceDamage(float dmg);
    void TakeElectroDamage(float dmg);
    void TakeFireDamage(float dmg);

    void TakeLaserDamage(float dmg);
    



}
