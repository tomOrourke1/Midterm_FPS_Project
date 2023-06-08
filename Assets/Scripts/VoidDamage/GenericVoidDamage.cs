using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericVoidDamage : MonoBehaviour, IVoidDamage
{
    public void FallIntoTheVoid()
    {
        GameObject.Destroy(gameObject);
    }
}
