using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour, IEntity
{
    [SerializeField] float freq;
    [SerializeField] float amp;

    Vector3 startingPos;

    /// <summary>
    /// Sets the starting position to the transforms position.
    /// </summary>
    private void Start()
    {
        startingPos = transform.position;
    }

    /// <summary>
    /// Updates the position to bob up and down.
    /// </summary>
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startingPos.y + Mathf.Sin(Time.time * freq) * amp, transform.position.z);
    }

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
                Destroy(gameObject);
            }
        }
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
