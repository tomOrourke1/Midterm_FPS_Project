using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{

    bool canAttack;
    bool isAttacking;






    private IEnumerator Attack()
    {
        isAttacking = true;


        yield return null;
        isAttacking = false;
    }


    void ShootAttack(GameObject bulletType, Vector3 spawnPos, Quaternion rotation)
    {
        Instantiate(bulletType, spawnPos, rotation);
    }



    public void QueueShootAttack(GameObject bulletType, Vector3 spawnPos, Quaternion rotation)
    {
        if(!isAttacking)
        {
            //StartCoroutine(Attack(bulletType, spawnPos, rotation));
        }
    }



}
