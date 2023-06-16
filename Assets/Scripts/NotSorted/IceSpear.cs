using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpear : MonoBehaviour
{
    [SerializeField] int damage;

    [SerializeField] float destroyTimer;
    

    // Start is called before the first frame update
    void Start()
    {
       // Destroy(gameObject, destroyTimer);
     

    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
    
}
