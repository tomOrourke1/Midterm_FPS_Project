using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpear : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] float destroyTimer;
     float totalCharge;
    [SerializeField] float totalChargeNeeded;

    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTimer);
     

    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(damage);
        }
    
    }
    
}
