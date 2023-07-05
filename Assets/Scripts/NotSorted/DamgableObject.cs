using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgableObject : MonoBehaviour, IEnvironment, IDamagable
{
    [SerializeField] Resource durabilityPool;
 
    [SerializeField] Renderer wallMeshRenderer;
    [SerializeField] Collider colliderC;
    [SerializeField] Material Dur3;
    [SerializeField] Material Dur2;
    [SerializeField] Material Dur1;
    Color wallColor;
    // Start is called before the first frame update
    private void Start()
    {
        durabilityPool.FillToMax();
        durabilityPool.OnResourceDepleted += Die;
        durabilityPool.OnResourceDecrease += CheckDurability;

        wallMeshRenderer = GetComponent<MeshRenderer>();
        wallMeshRenderer.sharedMaterial = Dur3;
    }

    private void OnDisable()
    {
        durabilityPool.OnResourceDepleted -= Die;
        durabilityPool.OnResourceDecrease -= CheckDurability;
    }
    public void TakeDamage(float dmg)
    {
        if (dmg > 0)
        {
            durabilityPool.Decrease(dmg);
         
         
            
        }
    }
    public void TakeIceDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeElectroDamage(float dmg)
    {
       
    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeLaserDamage(float dmg)
    {
      
    }
    public void StopObject()
    {
        this.enabled = false;
    }

    public void StartObject()
    {
        this.enabled = true;
    }

   
    public float GetCurrentHealth()
    {
        return durabilityPool.CurrentValue;
    }
    public void Die()
    {
       // Destroy(gameObject);

        wallMeshRenderer.enabled = false;
        colliderC.enabled = false;
    }
    void CheckDurability() 
    {
        if (durabilityPool.CurrentValue <= durabilityPool.MaxValue * (2 / 3))
        {
            wallMeshRenderer.sharedMaterial = Dur2;
        }
        if (durabilityPool.CurrentValue <= (durabilityPool.MaxValue * (1 / 3)))
        {
            wallMeshRenderer.sharedMaterial = Dur1;
        }
    }
}
