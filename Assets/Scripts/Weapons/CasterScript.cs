using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterScript : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] WindKinesis aero;
    [SerializeField] pyroBlast pyro;
    [SerializeField] IceKinesis ice;
    [SerializeField] LightningKinesis lightning;
    [SerializeField] TelekinesisController telekinesis;

    KinesisBase currentKenisis;

    public KinesisBase Current => currentKenisis;

    // Update is called once per frame
    void Update()
    {
        currentKenisis?.Fire();
    }

    public void SetCurrentKinesis(int opt)
    {
        if (currentKenisis != null && currentKenisis.isCasting)
            return;

        switch (opt)
        {
            case 0:
                SetAero();
                break;
            case 1:
                SetLightning();

                break;
            case 2:
                SetTelekinesis();

                break;
            case 3:
                SetPyro();

                break;
            case 4:
                SetIce();

                break;
            default:
                Debug.Log("ERROR YOU DID SOMETHING WRONG!");
                break;
        }
    }

    private void SetAero()
    {
        currentKenisis = aero;
    }

    private void SetPyro()
    {
        currentKenisis = pyro;
    }

    private void SetIce()
    {
        currentKenisis = ice;
    }

    private void SetLightning()
    {
        currentKenisis = lightning;
    }

    private void SetTelekinesis()
    {
        currentKenisis = telekinesis;
    }

}
