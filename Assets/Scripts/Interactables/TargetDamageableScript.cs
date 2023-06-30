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
        initMaterial = targetRenderer.material;
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
    public float GetCurrentHealth()
    {
        return currentTargetHp;
    }

    public void StartObject()
    {
        currentTargetHp = initalHP;
        targetRenderer.material = initMaterial;
        activated = false;
    }

    public void StopObject()
    {

    }

}
