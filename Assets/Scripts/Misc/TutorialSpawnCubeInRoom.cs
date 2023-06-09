using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawnCubeInRoom : MonoBehaviour
{


    [SerializeField] Transform spawnPos;
    [SerializeField] GameObject spawnObj;


    private void OnTriggerEnter(Collider other)
    {
        var tele = other.gameObject.GetComponent<ITelekinesis>();

        if(tele != null )
        {
            Destroy(other.gameObject);
            Instantiate(spawnObj, spawnPos.position, spawnPos.rotation);
        }
    }


}
