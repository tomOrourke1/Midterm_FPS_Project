using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZones : MonoBehaviour
{

    [SerializeField] bool DealsDamage;
    [SerializeField] int Damage;

    [SerializeField] bool EnemyDamageable;

    void OnTriggerEnter(Collider other)
    {
        IDamagable damageable = other.GetComponent<IDamagable>();

        if(damageable != null)
        {
            damageable.TakeDamage(Damage);
        }
        else
        {
            Destroy(other);
        }


        // Interfaces 
        //if (other.CompareTag("Player"))
        //{
        //    //Debug.Log("Player: " + other.name + "\nTag: " + other.gameObject.tag + "\nThing: " + other.GetType());
            
        //    if (DealsDamage)
        //    {
        //        // Damages the player
        //        PlayerDamage();
        //    }
        //    else
        //    {
        //        // This kills the player
        //        PlayerKill();
        //    }
            
        //} 
        //else if (other.CompareTag("Enemy"))
        //{
        //    //Debug.Log("Enemy: " + other.name + "\nTag: " + other.gameObject.tag + "\nThing: " + other.GetType());

        //    if (DealsDamage)
        //    {
        //        // Damages the enemy
        //        EnemyDamage(other);
        //    }
        //    else
        //    {
        //        // This Kills the enemy
        //        EnemyKill(other);
        //    }
        //}
        //else if (other.CompareTag("Prop"))
        //{
        //    //Debug.Log("Prop: " + other.name + "\nTag: " + other.gameObject.tag + "\nThing: " + other.GetType());

        //    if (DealsDamage)
        //    {
        //        // Does nothing to the object

        //    }
        //    else
        //    {
        //        // This deletes the object
        //        PropKill(other);
        //    }
        //}
        //else
        //{
        //    // Unhandled game objects go here
        //    Debug.LogError("Object: " + other.name + " tag?: " + other.gameObject.tag);
        //    Debug.Log("Other");
        //}
    }
    void PlayerKill()
    {
        // Get max health
        float maxHP = gameManager.instance.playerscript.GetPlayerMaxHP();
        float maxShield = gameManager.instance.playerscript.GetPlayerMaxShield();


        // Deal damage to the player equal to max HP
        gameManager.instance.playerscript.TakeDamage((int) maxHP + (int) maxShield);
    }
    void PlayerDamage()
    {
        gameManager.instance.playerscript.TakeDamage(Damage);
    }

    void EnemyKill(Collider other)
    {
        // Removes an enemy from the goal
        gameManager.instance.UpdateGameGoal(-1);

        // Destroys enemy
        Destroy(other.gameObject);
    }    
    void EnemyDamage(Collider other)
    {
        IDamagable damageable = other.GetComponent<IDamagable>();

        if (damageable != null && !EnemyDamageable)
        {
            damageable.TakeDamage(Damage);
        }
    }

    void PropKill(Collider other)
    {
        // Destroys prop
        Destroy(other.gameObject);
    }
}
