using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour, IEntity, IVoidDamage
{
    int spawnIndex;

    /// <summary>
    /// When the player enters the collider, check to make sure it is the player. 
    /// Then check to make sure the player can pickup the key. They must have one less than the max.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gets the current keys and will add keys until max keys reached.
            if (GameManager.instance.GetKeyChain().GetKeys() < GameManager.instance.GetKeyChain().GetMaxKeys())
            {
                GameManager.instance.GetKeyChain().addKeys(1);
                GameManager.instance.GetCurrentRoomManager()?.CallDeath(spawnIndex);
                Destroy(gameObject);
            }
        }
    }

    public void SetSpawner(int i)
    {
        spawnIndex = i;
    }


    /// <summary>
    /// When the room is respawned it will destroy all dropped keys.
    /// This should probably be redone so that it should be a toggleable delete key. 
    /// In case we place keys physically down in the world we don't want to delete them on room reset.
    /// </summary>
    public void Respawn()
    {
        Destroy(gameObject);
    }

    public void FallIntoTheVoid()
    {
        if (GameManager.instance.GetKeyChain().GetKeys() < GameManager.instance.GetKeyChain().GetMaxKeys())
        {
            GameManager.instance.GetKeyChain().addKeys(1);
            GameManager.instance.GetCurrentRoomManager().CallDeath(spawnIndex);
            Destroy(gameObject);
        }
    }
}
