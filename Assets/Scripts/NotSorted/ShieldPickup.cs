using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour, IEntity
{
    [Header("-----Healing Stuff-----")]
    [Range(0, 100)][SerializeField] int shieldAmount;

    private void OnTriggerEnter(Collider other)
    {

        var shield = other.GetComponent<IShieldReceiver>();

        if (shield != null)
        {
            shield.AddShield(shieldAmount);
            Destroy(gameObject);
        }

    }

    public void Respawn()
    {
        Destroy(gameObject);
    }

}
