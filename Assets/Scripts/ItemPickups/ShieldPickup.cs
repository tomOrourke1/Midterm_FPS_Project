using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour, IEntity
{
    [Header("Values")]
    [Range(0, 100)][SerializeField] int shieldAmount;

    [SerializeField] PickupSFX sfx;
    [SerializeField] ShrinkAndDelete sAD;
    private void OnTriggerEnter(Collider other)
    {
        var shield = other.GetComponent<IShieldReceiver>();

        if (shield != null && !GameManager.instance.GetPlayerResources().Shield.AtMax())
        {
            shield.AddShield(shieldAmount);
            sfx.Play_OneShot();
            sAD.Shrink();
        }
    }

    public void Respawn()
    {
        Destroy(gameObject);
    }

}
