using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZones : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player: " + other.name + "\nTag: " + other.gameObject.tag + "\nThing: " + other.GetType());
            // This kills the player
            PlayerKill();
        } 
        else if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy: " + other.name + "\nTag: " + other.gameObject.tag + "\nThing: " + other.GetType());
            // This Kills the enemy
            EnemyKill(other);
        }
        else if (other.CompareTag("Prop"))
        {
            Debug.Log("Prop: " + other.name + "\nTag: " + other.gameObject.tag + "\nThing: " + other.GetType());
            // This deletes the object
            PropKill(other);
        }
        else
        {
            Debug.LogError("Object: " + other.name + " tag?: " + other.gameObject.tag);
            Debug.Log("Other");
        }
    }
    void PlayerKill()
    {
        // Get max health
        float maxHP = gameManager.instance.playerscript.GetPlayerMaxHP();
        float maxShield = gameManager.instance.playerscript.GetPlayerMaxShield();


        // Deal damage to the player equal to max HP
        gameManager.instance.playerscript.TakeDamage((int) maxHP + (int) maxShield);
    }

    void EnemyKill(Collider other)
    {
        // Removes an enemy from the goal
        gameManager.instance.UpdateGameGoal(-1);

        // Destroys enemy
        Destroy(other.gameObject);
    }    

    void PropKill(Collider other)
    {
        // Destroys prop
        Destroy(other.gameObject);
    }
}
