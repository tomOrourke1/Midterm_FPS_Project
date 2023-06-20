using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpear : MonoBehaviour
{
    [SerializeField] int damage;

    [SerializeField] float destroyTimer;

    [SerializeField] GameObject breakParticles;
    

    // Start is called before the first frame update
    void Start()
    {
       // Destroy(gameObject, destroyTimer);
     

    }
    private void OnCollisionEnter(Collision collision)
    {
        var point = collision.GetContact(0).point;
        var dir = collision.GetContact(0).normal;
        Instantiate(breakParticles, point + dir * 0.2f, Quaternion.identity);
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
    
}
