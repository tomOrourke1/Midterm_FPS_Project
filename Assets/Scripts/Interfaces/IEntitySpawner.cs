using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntitySpawner
{
    /// <summary>
    /// Spawns the GameObject that is tied to the spawner.
    /// </summary>
    public void Spawn();
}
