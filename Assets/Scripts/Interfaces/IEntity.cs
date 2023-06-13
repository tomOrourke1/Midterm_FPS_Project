using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    /*
        This Will be used later in order to send entities to 
        the enemy pool whenever the player dies.
    */

    // Deletes the entities
    public void Respawn();

}
