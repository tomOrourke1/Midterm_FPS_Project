using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

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
    private void Awake()
    {
        durabilityPool.FillToMax();

        wallMeshRenderer = GetComponent<MeshRenderer>();
        wallMeshRenderer.material = Dur3;
    }


    private void OnEnable()
    {
        durabilityPool.OnResourceDepleted += Die;
        durabilityPool.OnResourceDecrease += CheckDurability;
        
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
        durabilityPool.FillToMax();

        wallMeshRenderer = GetComponent<MeshRenderer>();
        wallMeshRenderer.material = Dur3;
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
        if (durabilityPool.CurrentValue <= durabilityPool.MaxValue * .7f)
        {
            wallMeshRenderer.material = Dur2;
        }
        if (durabilityPool.CurrentValue <= (durabilityPool.MaxValue * .35f))
        {
            wallMeshRenderer.material = Dur1;
        }
    }
}
