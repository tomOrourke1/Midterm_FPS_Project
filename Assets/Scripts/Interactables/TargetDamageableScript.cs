using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetDamageableScript : MonoBehaviour, IDamagable
{

    [SerializeField] float maxTargetHp;

    [SerializeField] UnityEvent targetDeathEvent;
    [SerializeField] Renderer targetRenderer;
    [SerializeField] Material targeCheckedMaterial;
    float currentTargetHp;
    bool activated;

    private void Start()
    {
        currentTargetHp = maxTargetHp;
        activated = false;
    }
    public void TakeDamage(float dmg)
    {
        currentTargetHp -= dmg;
        if(currentTargetHp <= 0 && !activated) 
        {
            targetDeathEvent?.Invoke();
            targetRenderer.material = targeCheckedMaterial;
            activated = true;
        }
    }

    public float GetCurrentHealth()
    {
        return currentTargetHp;
    }
}
