using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandAnimEvents : MonoBehaviour
{

    [SerializeField] IceKinesis ice;

    public void ThrowIce()
    {
        ice.ThrowIce();
    }

    public void CanThrowIce()
    {
        ice.SetCanFire(true);
    }


}
