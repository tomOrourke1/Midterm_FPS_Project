using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour, IEntity
{
    [Header("-----Healing Stuff-----")]
    [Range(0, 100)][SerializeField] int healingAmount;

    private void OnTriggerEnter(Collider other)
    {
        var heal = other.GetComponent<IHealReciever>();

        if(heal != null)
        {
            heal.AddHealing(healingAmount);
            Destroy(gameObject);
        }

    }

    public void Respawn()
    {
        Destroy(gameObject);
    }
}
