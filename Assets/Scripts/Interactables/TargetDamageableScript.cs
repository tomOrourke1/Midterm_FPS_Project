using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetDamageableScript : MonoBehaviour, IDamagable, IEnvironment
{

    [SerializeField] float maxTargetHp;

    [SerializeField] UnityEvent targetDeathEvent;
    [SerializeField] Renderer targetRenderer;
    [SerializeField] Material targeCheckedMaterial;
    float currentTargetHp;
    bool activated;

    Material initMaterial;

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

    public void StartObject()
    {
        initMaterial = targetRenderer.material;
    }

    public void StopObject()
    {
        targetDeathEvent?.Invoke();
        activated = false;
    }

    public void ResetObject()
    {
        targetDeathEvent?.Invoke();
        targetRenderer.material = initMaterial;
        activated = false;
    }
}
