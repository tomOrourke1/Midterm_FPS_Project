using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeScript : MonoBehaviour
{
    [SerializeField] float damage;
    Collider weaponCollider;
    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
            }
        }
    }
}
