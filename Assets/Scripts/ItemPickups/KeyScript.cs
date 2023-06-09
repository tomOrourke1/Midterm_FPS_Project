using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour, IEntity
{
    int spawnIndex;
    bool collected = false;


    [SerializeField] ShrinkAndDelete shrinkScript;
    [SerializeField] PickupSFX sfxScript;

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
            if (GameManager.instance.GetKeyChain().GetKeys() < GameManager.instance.GetKeyChain().GetMaxKeys() && !collected)
            {
                if (!collected)
                {
                    collected = true;
                    GameManager.instance.GetKeyChain().addKeys(1);
                    //Debug.Log("FUCK");
                    GameManager.instance.GetCurrentRoomManager()?.CallDeath(spawnIndex);

                    sfxScript.Play_OneShot();
                    shrinkScript.Shrink();
                }
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


}
