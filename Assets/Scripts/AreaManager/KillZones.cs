using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZones : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.LogError("other name : " + other.name);
        if (other.isTrigger == false)
        {
            Kill(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.LogError("other name : " + other.name);
        if (other.isTrigger == false)
        {
            Kill(other);
        }
    }

    void Kill(Collider other)
    {
        IVoidDamage Voidable = other.GetComponent<IVoidDamage>();

        if (Voidable != null)
        {
            Voidable.FallIntoTheVoid();
        }
    }
}
