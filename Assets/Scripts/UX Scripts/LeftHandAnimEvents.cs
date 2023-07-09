using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandAnimEvents : MonoBehaviour
{

    [SerializeField] IceKinesis ice;
    [SerializeField] LightningKinesis lightning;
    [SerializeField] pyroBlast pyro;
    [SerializeField] WindKinesis aero;

    public void ThrowIce()
    {
        ice.ThrowIce();
    }

    public void CanThrowIce()
    {
        ice.SetCanFire(true);
    }

    public void StartLightning()
    {
        lightning.StartLightning();
    }


    public void FireHold()
    {
        pyro.SetIsReady(true);
    }
    public void FIreThrow()
    {
        pyro.SetCanActive(true);
    }
    public void AeroHold()
    {
        aero.SetIsReady(true);
    }
    public void AeroThrow()
    {
        aero.SetCanActive(true);
    }

}
