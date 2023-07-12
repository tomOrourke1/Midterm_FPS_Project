using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour, IEntity
{
    [Header("Values")]
    [Range(0, 100)][SerializeField] int healingAmount;
    [SerializeField] PickupSFX sfx;
    [SerializeField] ShrinkAndDelete sAD;

    private void OnTriggerEnter(Collider other)
    {
        var heal = other.GetComponent<IHealReciever>();

        if(heal != null && !GameManager.instance.GetPlayerResources().Health.AtMax())
        {
            heal.AddHealing(healingAmount);
            sfx.Play_OneShot();
            sAD.Shrink();
        }
    }

    public void Respawn()
    {
        Destroy(gameObject);
    }
}
