using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawners : MonoBehaviour
{
    [Header("----- Spawned Entity -----")]
    [SerializeField] GameObject Entity;

    bool myEnemyIsDead = false;

    private void Start()
    {
        GetObject();
    }

    public GameObject GetObject()
    {
        return Entity;
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }

    public void MyEnemyDied()
    {
        myEnemyIsDead = true;
    }

    public bool IsMyEnemyDead()
    {
        return myEnemyIsDead;
    }

    public void ResetEnemyDeath()
    {
        myEnemyIsDead = false;
    }

    public void DisableSpawner()
    {
        gameObject.SetActive(false);
        //enabled = false;
        //Debug.LogError("Does this do the disable????");
    }


}
