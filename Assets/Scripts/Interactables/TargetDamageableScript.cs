using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetDamageableScript : MonoBehaviour, IDamagable
{

    [SerializeField] int maxTargetHp;

    [SerializeField] UnityEvent targetDeathEvent;
    [SerializeField] Renderer targetRenderer;
    [SerializeField] Material targeCheckedMaterial;
    int currentTargetHp;
    bool activated;

    private void Start()
    {
        currentTargetHp = maxTargetHp;
        activated = false;
    }
    public void TakeDamage(int dmg)
    {
        currentTargetHp -= dmg;
        if(currentTargetHp <= 0 && !activated) 
        {
            targetDeathEvent?.Invoke();
            targetRenderer.material = targeCheckedMaterial;
            activated = true;
        }
    }
}
