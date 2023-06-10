using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/enemies", order = 1)]
public class TestScriptableObject : ScriptableObject, IDamagable
{
    public float health;
    public float speed;
    public float tirdness;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }
}
