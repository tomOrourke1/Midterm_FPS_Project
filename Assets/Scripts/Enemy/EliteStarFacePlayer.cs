using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteStarFacePlayer : MonoBehaviour
{
    private void Update()
    {
        var playerPos = GameManager.instance.GetPlayerPOS();
        var dir = playerPos - transform.position;
        dir.y = 0;
        dir.Normalize();

        transform.localRotation = Quaternion.LookRotation(-dir);
    }
}
