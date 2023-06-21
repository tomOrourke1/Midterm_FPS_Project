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

    [SerializeField] bool multiPress;

    Material initMaterial;

    float initalHP;



    private void Start()
    {
        currentTargetHp = maxTargetHp;
        initalHP = currentTargetHp;
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
        if(multiPress)
        {
            currentTargetHp = initalHP;
            activated = false;
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
        activated = false;
    }

    public void ResetObject()
    {
        currentTargetHp = initalHP;
        targetRenderer.material = initMaterial;
        activated = false;
    }
}
