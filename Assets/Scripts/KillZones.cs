using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZones : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        IVoidDamage Voidable = other.GetComponent<IVoidDamage>();

        if(Voidable != null)
        {
            Voidable.FallIntoTheVoid();
        }

    }
}
